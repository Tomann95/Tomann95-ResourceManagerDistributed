using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ResourceManager.API.Models;

namespace ResourceManager.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Asset> Assets => Set<Asset>();

}
