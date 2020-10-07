using System;
using System.Threading;
using System.Threading.Tasks;
using Garage.Email.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Garage.Email.SendGrid.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet]
        public async Task Get(string toEmail, string toName, CancellationToken cancellationToken = default)
        {
            await _emailService.SendAsync(toEmail, toName, "hello", "hello text", "<b>hello bold text</b>", cancellationToken);
        }
    }
}
