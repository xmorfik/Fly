using AutoMapper;
using Fly.Core.Entities;
using Fly.Data;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Claims;

namespace Fly.IdentityServer.Pages.Account.Register;

[Authorize(Roles = "Administrator")]
public class ManagerModel : PageModel
{
    [BindProperty]
    public ManagerForRegistrationDto ManagerForRegistrationDto { get; set; }
    public string ErrorMessage { get; set; }

    private readonly UserManager<User> _userManager;
    private readonly FlyDbContext _db;
    private readonly IMapper _mapper;
    private readonly ClientUriConfiguration _clientConfiguration;

    public ManagerModel(UserManager<User> userManager,
        SignInManager<User> signInManager,
        FlyDbContext db,
        IOptions<ClientUriConfiguration> clientUri,
        IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
        _db = db;
        _clientConfiguration = clientUri.Value;
    }

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _mapper.Map<User>(ManagerForRegistrationDto);
        var result = await _userManager.CreateAsync(user, ManagerForRegistrationDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim("Role", "Manager"));
            await _userManager.AddClaimAsync(user, new Claim("Airline", ManagerForRegistrationDto.AirlineId));
            var manager = _mapper.Map<Manager>(ManagerForRegistrationDto);
            manager.UserId = user.Id;

            try
            {
                await _db.Managers.AddAsync(manager);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                Log.Error(ex.Message);
            }

            return Redirect(_clientConfiguration.Uri);
        }

        ErrorMessage = result.Errors.FirstOrDefault().Description;

        return Page();
    }
}
