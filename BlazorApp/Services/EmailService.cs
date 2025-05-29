using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string smtpServer = "smtp.gmail.com"; // הגדרת כתובת שרת SMTP לשליחת מיילים דרך Gmail
    private readonly int smtpPort = 587; // הגדרת פורט ה-SMTP לשימוש עם TLS
    private readonly string smtpUsername = "razperli100@gmail.com"; // כתובת האימייל שמשמשת כמשתמש להתחברות לשרת
    private readonly string smtpPassword = Environment.GetEnvironmentVariable("EMAIL_APP_PASSWORD"); // טעינת סיסמת האפליקציה מתוך משתנה סביבה לשמירה על אבטחה


    /// <summary>
    /// Sends an email message asynchronously.
    /// </summary>
    /// <param name="recipientEmail">The email address to send the message to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The plain text body of the email.</param>
    public async Task SendEmailAsync(string to, string subject, string body, string from)
    {
        using (var client = new SmtpClient(smtpServer, smtpPort)) // יצירת מופע SmtpClient עם פרטי השרת והפורט
        {
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword); // קביעת פרטי הזדהות (שם משתמש וסיסמה)
            client.EnableSsl = true; // הפעלת הצפנת SSL לשליחת המייל בצורה מאובטחת

            var mailMessage = new MailMessage(from, to, subject, body); // יצירת אובייקט מייל עם פרטי השולח, הנמען, נושא ותוכן ההודעה
            await client.SendMailAsync(mailMessage); // שליחת המייל באופן אסינכרוני
        }
    }
}

