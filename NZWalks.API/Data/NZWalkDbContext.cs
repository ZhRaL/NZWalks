using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalkDbContext : DbContext
{
    public NZWalkDbContext(DbContextOptions<NZWalkDbContext> options) : base(options)
    {
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Image> Images { get; set; }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seed data for Difficulties
        var difficulties = new List<Difficulty>()
        {
            new Difficulty
            {
                Id = Guid.Parse("0eaa4913-c760-4981-baf6-9c0a5b81addc"),
                Name = "Easy"
            },
            new Difficulty
            {
                Id = Guid.Parse("672bb987-2f0b-4308-9e35-9dbac5d2cd9a"),
                Name = "Medium"
            },
            new Difficulty
            {
                Id = Guid.Parse("fb4f04bc-2615-496e-8b86-299509acc4a5"),
                Name = "Hard"
            }
        };
        modelBuilder.Entity<Difficulty>().HasData(difficulties);
        
        // Seed data for Regions
        var regions = new List<Region>()
        {
            new Region
            {
                Id = Guid.Parse("d8df10ac-4da7-490f-b99d-c23c48893415"),
                Name = "Auckland",
                Code = "AKL",
                RegionImageUrl = "some-image.png"
            }
        };
        
        modelBuilder.Entity<Region>().HasData(regions);
    }
}