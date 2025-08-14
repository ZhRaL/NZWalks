using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models;

namespace NZWalks.API.Data;

public class NZWalkDbContext : DbContext
{
    public NZWalkDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Region> Regions { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Walk> Walks { get; set; }
}