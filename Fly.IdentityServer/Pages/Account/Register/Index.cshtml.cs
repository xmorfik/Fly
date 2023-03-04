using AutoMapper;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Fly.Core.Entities;
using Fly.Data;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Fly.IdentityServer.Pages.Account.Register;

public class IndexModel : PageModel
{
    [BindProperty]
    public UserForRegistrationDto UserForRegistrationDto { get; set; }
    public string ErrorMessage { get; set; }

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly FlyDbContext _db;
    private readonly IMapper _mapper;
    private readonly ClientUriConfiguration _clientConfiguration;
    private readonly IConfiguration _configuration;

    public IndexModel(UserManager<User> userManager,
        SignInManager<User> signInManager,
        FlyDbContext db,
        IMapper mapper,
        IOptions<ClientUriConfiguration> clientUri,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
        _db = db;
        _clientConfiguration = clientUri.Value;
        _configuration = configuration;
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

        var user = _mapper.Map<User>(UserForRegistrationDto);
        user.UserName = UserForRegistrationDto.UserName;
        var result = await _userManager.CreateAsync(user, UserForRegistrationDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim("Role", "Passenger"));
            var passenger = _mapper.Map<Passenger>(UserForRegistrationDto);
            passenger.UserId = user.Id;
            try
            {
                await _db.Passengers.AddAsync(passenger);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                Log.Error(ex.Message);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var bytes = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(bytes);
            var confirmationLink = "https://" + Request.Host.ToString() + $"/account/confirmemail?token={validToken}&email={user.Email}";

            var emailClient = new EmailClient(_configuration["AzureEmailCommunicationService:ConnectionString"]);
            var emailContent = new EmailContent("Confrim Email");
            emailContent.PlainText = confirmationLink;
            var emails = new List<EmailAddress> { new EmailAddress(user.Email) };
            var emeilRecipients = new EmailRecipients(emails);
            var emailMessage = new EmailMessage("DoNotReply@7badc868-6ea4-4c4a-9840-2ee9bf0540c7.azurecomm.net", emailContent, emeilRecipients);
            var emailResult = await emailClient.SendAsync(emailMessage, CancellationToken.None);

            return Redirect(_clientConfiguration.Uri);
        }

        ErrorMessage = result.Errors.FirstOrDefault().Description;

        return Page();
    }
}
