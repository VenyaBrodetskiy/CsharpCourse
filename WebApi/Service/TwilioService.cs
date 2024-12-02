namespace WebApi.Service;

public class TwilioService(ILogger<TwilioService> logger)
{
    public Task SendSmsAsync(string phoneNumber, string messageBody, string senderId)
    {
        logger.LogInformation("Sending SMS: From {senderId} to {phoneNumber}: {messageBody}", senderId, phoneNumber, messageBody);
        return Task.CompletedTask;
    }

    public Task SendPushAsync(string deviceToken, string title, string body, Dictionary<string, string>? metadata = null)
    {
        logger.LogInformation("Sending Push Notification to {deviceToken}: {title} - {body}, Data: {metadata}",
            deviceToken, title, body, metadata != null ? string.Join(", ", metadata.Select(kv => $"{kv.Key}: {kv.Value}")) : "No Data");
        return Task.CompletedTask;
    }
}
