using Azure;
using Azure.Communication.Email;
using email_test.Services.Interfaces;

namespace email_test.Services.Implementation;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<OperationResult<EmailSendResult>> SendEmail(string recipient, string subject, string htmlContent)
    {
        string connectionString = _configuration.GetValue<string>("Email:ConnectionString");
        EmailClient emailClient = new EmailClient(connectionString);
        
        string sender = _configuration.GetValue<string>("Email:EmailClient");;

        try
        {
            Console.WriteLine("Sending email...");

            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                WaitUntil.Completed,
                sender,
                recipient,
                subject,
                htmlContent);
            
            EmailSendResult emailSendResult = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            // Get the OperationId so that it can be used for tracking the message for troubleshooting
            string operationId = emailSendOperation.Id;
            Console.WriteLine($"Email operation id = {operationId}");

            return new OperationResult<EmailSendResult>
            {
                Code = 200,
                Description = "Email successfully sent, yay!",
                IsSuccessful = true,
                ResultValue = emailSendResult,
            };
        }
        catch (RequestFailedException ex)
        {
            // OperationID is contained in the exception message and can be used for troubleshooting purposes, happens when there's an issue in the connection with the email sender
            Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");

            return new OperationResult<EmailSendResult>
            {
                Code = 500,
                Description = "Issue with email domain :-(",
                IsSuccessful = false,
            };
        }
    }
}

