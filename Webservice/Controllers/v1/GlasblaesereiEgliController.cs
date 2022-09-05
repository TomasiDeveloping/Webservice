using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Webservice.Helper;
using Webservice.Interfaces;
using Webservice.Models;
using Webservice.Services;

namespace Webservice.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[Authorize(Roles = "User")]
public class GlasblaesereiEgliController : ControllerBase
{
    private readonly EmailService _emailService;
    private readonly ILogService _logService;

    public GlasblaesereiEgliController(IOptions<EmailSettings> options, ILogService logService, IConfiguration configuration)
    {
        _logService = logService;
        _emailService = new EmailService(options, configuration);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<bool>> SendTestMail()
    {
        const string receiverAddress = "info@glasblaeserei-egli.ch";
        var subject = $"Funktionstest {DateTime.Now:dd.MMMM HH:mm}";
        var message = EmailMessagesService.CreateTestMailMessage();
        try
        {
            await _emailService.SendEmailAsync(receiverAddress, message, subject);
            //await _emailService.SendMailWithSendGridAsync(receiverAddress, message, subject);
            await _logService.LogAsync(new Log
            {
                Requester = User.Claims.First().Value,
                RequestMethod = "GlasblaesereiEgli -> SendTestMail()",
                RequestDate = DateTime.Now,
                LogTypeId = (int) Constantes.LogTypes.SUCCESS
            });
            return Ok(true);
        }
        catch (Exception e)
        {
            await _logService.LogAsync(new Log
            {
                Requester = User.Claims.First().Value,
                RequestMethod = "GlasblaesereiEgli -> SendTestMail()",
                RequestDate = DateTime.Now,
                LogTypeId = (int) Constantes.LogTypes.ERROR,
                ErrorMessage = e.Message,
                InnerException = e.InnerException?.ToString()
            });
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Mail mail)
    {
        try
        {
            const string subject = "Kontaktformular Webseite";
            var message = EmailMessagesService.CreateContactFormMailMessage(mail);
            var checkSend =
                await _emailService.SendMailWithSendGridAsync(HttpUtility.HtmlEncode(mail.ReceiverAddress), message,
                    subject);
                //await _emailService.SendEmailAsync(HttpUtility.HtmlEncode(mail.ReceiverAddress), message, subject);
            if (!checkSend) return BadRequest("Email konnte nicht gesendet werden.");
            return Ok(checkSend);
        }
        catch (Exception e)
        {
            await _logService.LogAsync(new Log
            {
                Requester = User.Claims.First().Value,
                RequestMethod = "GlasblaesereiEgli -> Send Contact mail",
                RequestDate = DateTime.Now,
                LogTypeId = (int) Constantes.LogTypes.ERROR,
                ErrorMessage = e.Message,
                InnerException = e.InnerException?.ToString()
            });
            return BadRequest("Email konnte nicht gesendet werden.");
        }
    }
}