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

public class SmsNotificationService : INotificationService
{
    public Task SendAsync(string recipient, string message)
    {
        Console.WriteLine($"Sending SMS to {recipient}: {message}");
        return Task.CompletedTask;
    }
}

public class PushNotificationService : INotificationService
{
    public Task SendAsync(string recipient, string message)
    {
        Console.WriteLine($"Sending Push Notification to {recipient}: {message}");
        return Task.CompletedTask;
    }
}
