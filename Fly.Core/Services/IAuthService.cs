using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Fly.Core.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
}

