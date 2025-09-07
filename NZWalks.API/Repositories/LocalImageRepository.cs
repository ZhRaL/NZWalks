using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class LocalImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly NZWalkDbContext _dbContext;

    public LocalImageRepository(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor, NZWalkDbContext dbContext)
    {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
        using var fileStream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(fileStream);

        var urlFilePath =
            $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

        image.FilePath = urlFilePath;
        
        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();
        
        return image;
    }
}