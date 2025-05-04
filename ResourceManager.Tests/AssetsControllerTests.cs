using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ResourceManager.API.Controllers;
using ResourceManager.API.Data;
using ResourceManager.API.Models;
using ResourceManager.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Xunit;

namespace ResourceManager.Tests;

public class AssetsControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsListOfAssets()
    {
        // Arrange – baza danych in-memory
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new AppDbContext(options);
        context.Assets.Add(new Asset { Name = "Test Laptop", Type = "Laptop" });
        await context.SaveChangesAsync();

        var mockHub = new Mock<IHubContext<SyncHub>>();
        var controller = new AssetsController(context, mockHub.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var assets = Assert.IsAssignableFrom<IEnumerable<Asset>>(okResult.Value);
        Assert.Single(assets);
    }
}

