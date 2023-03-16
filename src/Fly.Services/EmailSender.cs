using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Fly.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class EmailSender : IEmailSender<EmailMessage>
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailSender> _logger;
    public EmailSender(
        IConfiguration configuration,
        ILogger<EmailSender> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task Send(EmailMessage emailMessage)
    {
        var emailClient = new EmailClient(_configuration["AzureEmailCommunicationService:ConnectionString"]);
        return emailClient.SendAsync(emailMessage, CancellationToken.None);
    }
}
