using AutoMapper;
using Azure.Communication.Email.Models;
using Fly.Core;
using Fly.Core.Entities;
using Fly.Core.Services;
using Fly.Data;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace Fly.IdentityServer.Pages.Account.Register;

public class IndexModel : PageModel
{
    [BindProperty]
    public UserForRegistrationDto UserForRegistrationDto { get; set; }
    public string ErrorMessage { get; set; }

    private readonly UserManager<User> _userManager;
    private readonly FlyDbContext _db;
    private readonly ClientUriConfiguration _clientConfiguration;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender<EmailMessage> _emailSender;

    public IndexModel(
        UserManager<User> userManager,
        FlyDbContext db,
        IMapper mapper,
        IOptions<ClientUriConfiguration> clientUri,
        IConfiguration configuration,
        IEmailSender<EmailMessage> emailSender)
    {
        _userManager = userManager;
        _mapper = mapper;
        _db = db;
        _clientConfiguration = clientUri.Value;
        _configuration = configuration;
        _emailSender = emailSender;
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
            await _userManager.AddClaimAsync(user, new Claim(Scopes.Role, Claims.Airline));
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

            var emailContent = new EmailContent("Confrim Email");
            emailContent.PlainText = confirmationLink;
            var emails = new List<EmailAddress> { new EmailAddress(user.Email) };
            var emeilRecipients = new EmailRecipients(emails);
            var emailMessage = new EmailMessage(_configuration["SenderDomain"], emailContent, emeilRecipients);

            _emailSender.Send(emailMessage);

            return Redirect(_clientConfiguration.Uri);
        }

        ErrorMessage = result.Errors.FirstOrDefault().Description;

        return Page();
    }
}
