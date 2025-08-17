using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SQLRegionRepository : IRegionRepository
{
    private readonly NZWalkDbContext dbContext;

    public SQLRegionRepository(NZWalkDbContext context)
    {
        dbContext = context;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public Task<Region> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Region> SaveAsync(Region region)
    {
        throw new NotImplementedException();
    }

    public Task<Region> UpdateAsync(Region region)
    {
        throw new NotImplementedException();
    }

    public Task<Region> DeleteAsync(Region region)
    {
        throw new NotImplementedException();
    }
}