namespace WebApiControllers.Service;

public interface INotificationFactory
{
    INotificationService GetNotificationService(string type);
}
public class NotificationFactory(IServiceProvider serviceProvider) : INotificationFactory
{
    public INotificationService GetNotificationService(string type)
    {
        return type.ToLower() switch
        {
            "email" => serviceProvider.GetRequiredService<EmailNotificationService>(),
            "sms" => serviceProvider.GetRequiredService<SmsNotificationService>(),
            "push" => serviceProvider.GetRequiredService<PushNotificationService>(),
            _ => throw new NotSupportedException($"Notification type '{type}' is not supported")
        };
    }
}

