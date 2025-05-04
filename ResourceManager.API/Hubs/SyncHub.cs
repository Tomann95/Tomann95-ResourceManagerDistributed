using Microsoft.AspNetCore.SignalR;

namespace ResourceManager.API.Hubs;

public class SyncHub : Hub
{
    public async Task SendAssetUpdate(string message)
    {
        await Clients.All.SendAsync("AssetUpdated", message);
    }
}

