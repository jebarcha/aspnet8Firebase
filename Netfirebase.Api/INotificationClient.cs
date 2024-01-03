namespace Netfirebase.Api;

public interface INotificationClient
{
    Task ReceiveNotification(string message);
}
