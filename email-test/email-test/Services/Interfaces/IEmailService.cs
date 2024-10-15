using Azure.Communication.Email;

namespace email_test.Services.Interfaces;

public interface IEmailService
{
    public  Task<OperationResult<EmailSendResult>> SendEmail(string recipient, string subject, string htmlContent);
}