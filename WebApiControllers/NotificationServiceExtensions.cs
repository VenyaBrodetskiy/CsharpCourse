using WebApiControllers.Service;

namespace WebApiControllers;

public static class NotificationServiceExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services)
    {
        services.AddSingleton<EmailNotificationService>();
        services.AddSingleton<SmsNotificationService>();
        services.AddSingleton<PushNotificationService>();

        services.AddSingleton<INotificationFactory, NotificationFactory>();

        return services;
    }
}
