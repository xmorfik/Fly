using AutoMapper;
using Fly.Core.Entities;
using Fly.Data;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using Serilog;
using System.Security.Claims;

namespace Fly.IdentityServer.Pages.Account.Register;

public class IndexModel : PageModel
{
    [BindProperty]
    public UserForRegistrationDto UserForRegistrationDto { get; set; }

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly FlyDbContext _db;
    private readonly IMapper _mapper;
    private readonly ClientUriConfiguration _clientConfiguration;

    public IndexModel(UserManager<User> userManager, 
        SignInManager<User> signInManager,
        FlyDbContext db,
        IMapper mapper, 
        IOptions<ClientUriConfiguration> clientUri)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
        _db = db;
        _clientConfiguration = clientUri.Value;
    }

    public async Task<IActionResult> OnGet()
    {
        await _signInManager.SignOutAsync();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _mapper.Map<User>(UserForRegistrationDto);
        user.UserName = UserForRegistrationDto.UserName;
        var result = await _userManager.CreateAsync(user, UserForRegistrationDto.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddClaimAsync(user, new Claim("Role", "Passenger"));
            var passenger = _mapper.Map<Passenger>(UserForRegistrationDto);
            passenger.UserId = user.Id;
            try
            {
                await _db.Passengers.AddAsync(passenger);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
               await _userManager.DeleteAsync(user);
               Log.Error(ex.Message);
            }

            return Redirect(_clientConfiguration.Uri);
        }

        return Page();
    }
}
