using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string smtpServer = "smtp.gmail.com"; // Change if using another provider
    private readonly int smtpPort = 587;
    private readonly string smtpUsername = "razperli100@gmail.com"; // Your email
    private readonly string smtpPassword = Environment.GetEnvironmentVariable("EMAIL_APP_PASSWORD"); // Use App Password (see below)


    /// <summary>
    /// Sends an email message asynchronously.
    /// </summary>
    /// <param name="recipientEmail">The email address to send the message to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The plain text body of the email.</param>
    public async Task SendEmailAsync(string to, string subject, string body, string from)
    {
        using (var client = new SmtpClient(smtpServer, smtpPort))
        {
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage(from, to, subject, body);
            await client.SendMailAsync(mailMessage);
        }
    }
}
