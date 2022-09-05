using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Webservice.Helper;

namespace Webservice.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;
    private readonly string _host;
    private readonly string _password;
    private readonly int _port;
    private readonly string _senderAddress;
    private readonly string _userName;

    public EmailService(IOptions<EmailSettings> conf, IConfiguration  configuration)
    {
        _configuration = configuration;
        _host = conf.Value.Host;
        _port = conf.Value.Port;
        _userName = conf.Value.UserName;
        _password = conf.Value.Password;
        _senderAddress = conf.Value.SenderAddress;
    }

    public async Task<bool> SendEmailAsync(string receiverAddress, string message, string subject)
    {
        var smtpServer = new SmtpClient(_host, _port);
        var mail = new MailMessage();

        smtpServer.EnableSsl = false;
        smtpServer.Credentials = new NetworkCredential(_userName, _password);

        mail.From = new MailAddress(_senderAddress);
        mail.To.Add(receiverAddress);
        mail.Subject = subject;
        mail.Body = message;
        mail.IsBodyHtml = true;

        await smtpServer.SendMailAsync(mail);
        return true;
    }

    public async Task<bool> SendMailWithSendGridAsync(string receiverAddress, string message, string subject)
    {
        var apiKey = _configuration.GetValue<string>("SendGridApiKey");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_senderAddress, _senderAddress);
        var to = new EmailAddress(receiverAddress, receiverAddress);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, message);
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}