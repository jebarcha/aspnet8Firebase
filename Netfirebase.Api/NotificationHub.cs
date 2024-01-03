using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Netfirebase.Api;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    public override Task OnConnectedAsync()
    {
        Clients.Client(Context.ConnectionId).ReceiveNotification(
            $"Thanks for all {Context.User?.Identity?.Name}"
        );

        return base.OnConnectedAsync();
    }
}
