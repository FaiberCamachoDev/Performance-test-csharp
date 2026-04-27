namespace Performance_test_csharp.Interfaces;

public interface IEmailService
{
    Task SendReservationtCreatedAsync(string toEmail, string userName, 
        string spaceName, DateTime date, TimeSpan startTime, TimeSpan endTime);
    
    Task SendCancelledAsync(string toEmail, string userName, 
        string spaceName, DateTime date);
}