namespace ResourceManager.API.Models;

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = ""; 
    public string Status { get; set; } = "Dostępny"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

