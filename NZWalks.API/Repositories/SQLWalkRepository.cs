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

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        Walk? currentWalk = await _dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
        if(currentWalk == null) return null;
        
        currentWalk.Region = walk.Region;
        currentWalk.Name = walk.Name;
        currentWalk.WalkImageUrl = walk.WalkImageUrl;
        currentWalk.Description = walk.Description;
        currentWalk.Difficulty = walk.Difficulty;
        currentWalk.LengthInKm = walk.LengthInKm;
        
        await _dbContext.SaveChangesAsync();
        return currentWalk;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        Walk? currentWalk = await _dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
        if(currentWalk == null) return null;
        _dbContext.Walks.Remove(currentWalk);
        await _dbContext.SaveChangesAsync();
        return currentWalk;
    }
}