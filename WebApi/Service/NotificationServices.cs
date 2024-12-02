namespace WebApi.Service;

public interface INotificationService
{
    Task SendAsync(string recipient, string message);
}

public class EmailNotificationService : INotificationService
{
    public Task SendAsync(string recipient, string message)
    {
        Console.WriteLine($"Sending Email to {recipient}: {message}");
        return Task.CompletedTask;
    }
}

public class SmsNotificationService(TwilioService twilio, UserDbService userDb, ILogger<SmsNotificationService> logger) : INotificationService
{
    public async Task SendAsync(string recipient, string message)
    {
        logger.LogInformation("Sending SMS to {recipient}: {message}", recipient, message);
        var user = userDb.GetUserInfo(recipient);
        await twilio.SendSmsAsync(user.PhoneNumber, message, "WebApiApp");
    }
}

public class PushNotificationService(TwilioService twilio, UserDbService userDb, ILogger<PushNotificationService> logger) : INotificationService
{
    public Task SendAsync(string recipient, string message)
    {
        logger.LogInformation("Sending Push Notification to {recipient}: {message}", recipient, message);
        var user = userDb.GetUserInfo(recipient);
        return twilio.SendPushAsync(user.DeviceToken, "WebApiApp", message);
    }
}
