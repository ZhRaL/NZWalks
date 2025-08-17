using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region> GetByIdAsync(Guid id);
    Task<Region> SaveAsync(Region region);
    Task<Region> UpdateAsync(Region region);
    Task<Region> DeleteAsync(Region region);
    
}