using Microsoft.AspNetCore.SignalR;
using Netfirebase.Api.Services.Authentication;
using Netfirebase.Api.Services.Products;

namespace Netfirebase.Api;

public class ServerNotifier : BackgroundService
{
    private static readonly TimeSpan period = TimeSpan.FromSeconds(5);
    private readonly IHubContext<NotificationHub, INotificationClient> _contextSR;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<ServerNotifier> _logger;

    public ServerNotifier(IHubContext<NotificationHub, INotificationClient> contextSR, ILogger<ServerNotifier> logger, IServiceScopeFactory scopeFactory)
    {
        _contextSR = contextSR;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(period);
        while (!stoppingToken.IsCancellationRequested && 
            await timer.WaitForNextTickAsync(stoppingToken));
        {
            var dateTime = DateTime.Now;
            _logger.LogInformation($"Executing {nameof(ServerNotifier)} {dateTime}");

            using var scope = _scopeFactory.CreateScope();
            var authenticationService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

            var user = await authenticationService.GetUserByEmail("paul.mccartney@gmail.com")!;

            if (user is not null)
            {
                var products = await productService.GetByName("A");
                var random = new Random();
                var indexRandom = random.Next(products.Count());
                var product = products[indexRandom];

                await _contextSR.Clients.User(user.FirebaseId!)
                    .ReceiveNotification($@"Product of the day: {product.Name} 
                    - Pay only {product.Price}");
            }


            //await _contextSR.Clients.All.ReceiveNotification($"Server time = {dateTime}");
        }
    }
}
