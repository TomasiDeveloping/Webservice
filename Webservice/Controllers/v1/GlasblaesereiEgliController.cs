using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Webservice.Helper;
using Webservice.Interfaces;
using Webservice.Models;
using Webservice.Services;

namespace Webservice.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize(Roles = "User")]
    public class GlasblaesereiEgliController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly ILogService _logService;

        public GlasblaesereiEgliController(IOptions<EmailSettings> options, ILogService logService)
        {
            _emailService = new EmailService(options);
            _logService = logService;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> SendTestMail()
        {
            const string receiverAddress = "info@glasblaeserei-egli.ch";
            var subject = $"Funktionstest {DateTime.Now:dd.MMMM HH:mm}";
            var message = $"<h2>E-Mail Test vom: {DateTime.Now:dd.MMMM} </h2><p>Automatischer E-Mail Test vom: " +
                          $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} </p></br><p>Liebe Gruss</p><p>Patrick</p>";
            try
            {
                await _emailService.SendEmailAsync(receiverAddress, message, subject);
                await _logService.Log(new Log()
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
                await _logService.Log(new Log()
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
    }
}