using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoEmailVerificationWithPaperCut.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController(IFluentEmail emailService) : ControllerBase
    {

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(SendMail mail)
        {
            SendResponse response = await emailService
                 .To(mail.ToEmail)
                 .Subject(mail.Subject)
                 .Body(mail.Message, isHtml: true)
                 .SendAsync();
            return response.Successful? Ok(response) : BadRequest(response);
        }
    }


    public record SendMail(string ToEmail, string Subject, string Message);
}
