using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authenticationService; 
    public AuthenticationController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await
        _authenticationService.RegisterUser(userForRegistration);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        return StatusCode(201);
    }
}
