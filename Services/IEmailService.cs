public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailDto model);
}