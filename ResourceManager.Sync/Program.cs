using ResourceManager.Sync;

var syncClient = new AssetSyncClient();
await syncClient.StartAsync();

Console.WriteLine("Naciśnij [Enter], aby zakończyć...");
Console.ReadLine();

await syncClient.StopAsync();
