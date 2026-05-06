using System.Net.Mail;

namespace TodoProjectUsingCleanArchitecture.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendWelcomeEmailAsync(string email, string username)
        {
            // In a real app, you'd use SmtpClient or a service like SendGrid
            // For now, we will simulate sending a "nice UI" email
            
            var htmlBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #ddd; border-radius: 10px; padding: 20px; background-color: #f9f9f9;'>
                    <h2 style='color: #4CAF50; text-align: center;'>Welcome to TodoPlatform, {username}!</h2>
                    <p style='font-size: 16px; color: #333;'>
                        We are thrilled to have you on board. Start managing your tasks efficiently and stay productive!
                    </p>
                    <div style='text-align: center; margin-top: 30px;'>
                        <a href='#' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; font-weight: bold;'>Go to Dashboard</a>
                    </div>
                    <p style='font-size: 12px; color: #777; margin-top: 40px; text-align: center;'>
                        If you didn't sign up for this account, please ignore this email.
                    </p>
                </div>";

            _logger.LogInformation("Sending Welcome Email to {Email} with body: {Body}", email, htmlBody);
            
            // To actually send it (if SMTP was configured):
            // var message = new MailMessage("noreply@todoplatform.com", email);
            // message.Subject = "Welcome to TodoPlatform";
            // message.Body = htmlBody;
            // message.IsBodyHtml = true;
            // using var client = new SmtpClient("smtp.example.com");
            // await client.SendMailAsync(message);

            await Task.CompletedTask;
        }
    }
}
