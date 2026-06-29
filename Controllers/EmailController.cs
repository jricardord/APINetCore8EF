using BusinessRules.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmail _email;

        public EmailController(IEmail email)
        {
            _email = email;
        }

        [HttpPost]
        public async Task<ActionResult> EnviarMail(string email, string title, string body)
        {
            await _email.SendEmail(email, title, body);
            return Ok();
        }
    }
}
