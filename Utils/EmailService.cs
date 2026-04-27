using MailKit.Net.Smtp;
using MimeKit;
using Performance_test_csharp.Interfaces;

namespace Performance_test_csharp.Utils;

public class EmailService : IEmailService
{
     private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendReservationtCreatedAsync(string toEmail, string userName, string spaceName, DateTime date, TimeSpan startTime,
        TimeSpan endTime)
    {
        var subject = "Reservation confirmed.";
        var body = $"""
            Hi {userName},

            a reservation has been created for {spaceName}.
            Date: {date:dd/MM/yyyy}
            Start Hour: {startTime:HH:mm}
            End Hour: {endTime:HH:mm}

            Please, arrive 10 minutes early.

            Sport management system
            """;
        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendCancelledAsync(string toEmail, string userName, string spaceName, DateTime date)
    {
        var subject = "Reservation cancelled";
        var body = $"""
            Hi {userName},

            The Sport place reservation of {date:dd/MM/yyyy} for {spaceName} has been cancelled.
            If you wish re-schedule, please contact us.

            E-mail: {toEmail}
            Sport management system
            """;
        await SendEmailAsync(toEmail, subject, body);
    }
    

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            var settings = _config.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                settings["SenderName"], settings["SenderEmail"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                settings["SmtpHost"],
                int.Parse(settings["SmtpPort"]!),
                bool.Parse(settings["UseSsl"]!));
            await client.AuthenticateAsync(
                settings["SenderEmail"], settings["Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email sent to {Email}: {Subject}", toEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sent email to {Email}", toEmail);
            // No relanzamos, el correo es funcionalidad secundaria, no debe romper el flujo
            //todo: en caso de error, queda atrapado en catch, no se envia el correo pero al usuario le aparece creada la reservacion
        }
    }
}