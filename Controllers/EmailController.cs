
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] EmailDto model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool result = await _emailService.SendEmailAsync(model);
                return Ok(result);
            }
        }
        catch (Exception)
        {

        }

        return BadRequest(ModelState);
    }
}
