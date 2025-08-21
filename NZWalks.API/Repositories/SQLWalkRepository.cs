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

    public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
    {
        var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

        // Filtering
        if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(w => w.Name.Contains(filterQuery));
            }
        }

        // Sorting
        if (string.IsNullOrEmpty(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
            }
            else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
            }
        }
        
        // Pagination
        var skipResult = (pageNumber - 1) * pageSize;

        return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
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
        if (currentWalk == null) return null;

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
        if (currentWalk == null) return null;
        _dbContext.Walks.Remove(currentWalk);
        await _dbContext.SaveChangesAsync();
        return currentWalk;
    }
}