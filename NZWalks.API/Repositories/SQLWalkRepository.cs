using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NZWalkDbContext _dbContext;

    public SQLWalkRepository(NZWalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Walk>> GetAllAsync()
    {
        var walks = await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        return walks;
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        var walk = await _dbContext.Walks
            .Include("Difficulty")
            .Include("Region")
            .FirstOrDefaultAsync(x => x.Id == id);
        return walk;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk region)
    {
        throw new NotImplementedException();
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}