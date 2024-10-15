using Azure.Communication.Email;
using email_test.Services;
using Microsoft.AspNetCore.Mvc;
using email_test.Services.Interfaces;

namespace email_test.Controllers;

[Route("[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("controller/sendEmail")]
    public async Task<ActionResult<OperationResult<EmailSendResult>>> SendEmail([FromBody] EmailDto emailDto)
    {
        var emailResult = await _emailService.SendEmail(emailDto.Recepient,emailDto.Subject, emailDto.HtmlContent);

        return emailResult;
    }
}

public class EmailDto
{
    public string Recepient { get; set; }
    public string Subject { get; set; }
    public string HtmlContent { get; set; }
}