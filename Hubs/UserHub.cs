using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SignalRProject.Hubs
{
    public class UserHub :Hub
    {
        public static int TotalViews { get; set; }
        public static int TotalUsers { get; set; }
        private readonly ILogger<UserHub> _logger;
        private readonly TelemetryClient _telemetryClient;
        public UserHub(ILogger<UserHub> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }
        public async Task NewWindowLoaded()
        {
            // Track a custom event
            _telemetryClient.TrackEvent("MessageSent", new Dictionary<string, string>
            {
                { "User", TotalViews.ToString() },
                { "Message", TotalViews.ToString() }
            });
            

            // Log information
            _logger.LogInformation("Message sent by {User}: {Message}", TotalViews, TotalViews);

            TotalViews++;
            await Clients.All.SendAsync("updateTotalVies", TotalViews);
        }
        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            _telemetryClient.TrackEvent("MessageSent-OnConnectedAsync-TotalUsers", new Dictionary<string, string>
            {
                { "User", TotalUsers.ToString() },
                { "Message", "OnConnectedAsync" }
            });

            // Log information
            _logger.LogInformation("Message sent by {User}: {Message}", TotalUsers, OnConnectedAsync);
            Clients.All.SendAsync("updateTotalUsers",TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
           
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
