using Microsoft.AspNetCore.SignalR.Client;

namespace ResourceManager.Sync;

public class AssetSyncClient
{
    private readonly HubConnection _connection;

    public AssetSyncClient()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7202/synchub") // musimy dodać SyncHub po stronie API
            .WithAutomaticReconnect()
            .Build();

        _connection.On<string>("AssetUpdated", message =>
        {
            Console.WriteLine($"[SYNC] Otrzymano aktualizację zasobu: {message}");
        });
    }

    public async Task StartAsync()
    {
        await _connection.StartAsync();
        Console.WriteLine("[SYNC] Połączono z SignalR");
    }

    public async Task StopAsync()
    {
        await _connection.StopAsync();
    }
}

