using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Fly.Services;

public sealed class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public AuthService(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
        return result;
    }
}
