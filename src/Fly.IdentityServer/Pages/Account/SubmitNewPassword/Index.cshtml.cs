using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text;

namespace Fly.IdentityServer.Pages.Account.SubmitNewPassword;

public class IndexModel : PageModel
{
    [BindProperty]
    public NewPasswordDto NewPasswordDto { get; set; } = new();
    public string ErrorMessage { get; set; }

    private readonly UserManager<User> _userManager;

    public IndexModel(
        UserManager<User> userManager,
        IOptions<ClientUriConfiguration> clientUri)
    {
        _userManager = userManager;
    }

    public void OnGet(string token, string username)
    {
        if (token != null)
        {
            NewPasswordDto.Token = token;
        }

        if (username != null)
        {
            NewPasswordDto.UserName = username;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        if (NewPasswordDto.Token == null)
        {
            return Redirect("/home/error");
        }

        var user = await _userManager.FindByNameAsync(NewPasswordDto.UserName);
        if (user == null)
        {
            return Redirect("/home/error");
        }

        if (NewPasswordDto.Password == null || NewPasswordDto.ConfirmPassword == null)
        {
            return Page();
        }

        var bytes = WebEncoders.Base64UrlDecode(NewPasswordDto.Token);
        var validToken = Encoding.UTF8.GetString(bytes);

        if (NewPasswordDto.Password != NewPasswordDto.ConfirmPassword)
        {
            ErrorMessage = "Password mismatch";
            return Page();
        }

        var result = await _userManager.ResetPasswordAsync(user, validToken, NewPasswordDto.Password);
        if (result.Succeeded)
        {
            return RedirectToPage("success");
        }

        ErrorMessage = result.Errors.FirstOrDefault().Description;

        return Page();
    }
}
