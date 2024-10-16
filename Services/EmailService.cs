using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly IStrapiService _strapiService;
    private string Email { get; }
    private string Host { get; }
    private int Port { get; }

    private string Password { get; }

    public EmailService(IConfiguration configuration, IStrapiService strapiService)
    {
        _strapiService = strapiService;
        Email = configuration["SMTP:Email"];
        Port = Convert.ToInt16(configuration["SMTP:Port"]);
        Host = configuration["SMTP:Host"];
        Password = configuration["SMTP:Password"];
    }
    public async Task<bool> SendEmailAsync(EmailDto emailDto)
    {
        try
        {
            if (emailDto.IsBodyHtml)
            {
                var template = await _strapiService.GetEmailTemplateByNameAsync(emailDto.TemplateName);
                emailDto.Content = TemplateParser.ParseEmailTemplate(template.Content, emailDto.TemplateData);
            }

            SmtpClient client = new(Host, Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Email, Password)
            };
            MailMessage mailMessage = new()
            {
                From = new MailAddress(Email)
            };
            mailMessage.To.Add(emailDto.To);
            if (emailDto.CC != null)
            {
                foreach (var ccAddress in emailDto.CC)
                {
                    mailMessage.CC.Add(new MailAddress(ccAddress));
                }
            }
            if (emailDto.BCC != null)
            {
                foreach (var bccAddress in emailDto.BCC)
                {
                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                }
            }
            mailMessage.Subject = emailDto.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = emailDto.Content;
            await client.SendMailAsync(mailMessage);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}