using AutoMapper;
using Fly.Core.Entities;
using Fly.Data;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Security.Claims;

namespace Fly.IdentityServer.Pages.Account.Register;

[Authorize(Roles = "Administrator")]
public class ManagerModel : PageModel
{
    [BindProperty]
    public ManagerForRegistrationDto ManagerForRegistrationDto { get; set; }
    private readonly UserManager<User> _userManager;
    private readonly FlyDbContext _db;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;

    public ManagerModel(UserManager<User> userManager,
        SignInManager<User> signInManager,
        FlyDbContext db,
        IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
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

            return Redirect("https://localhost:5002");
        }
        return Page();
    }
}
