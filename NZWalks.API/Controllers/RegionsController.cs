using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalkDbContext _dbContext;
    private readonly IRegionRepository _regionRepository;

    public RegionsController(NZWalkDbContext dbContext, IRegionRepository regionRepository)
    {
        _dbContext = dbContext;
        _regionRepository = regionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRegions()
    {
        var regions = await _regionRepository.GetAllAsync();

        var regionsDto = regions.Select(region => new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl
        }).ToList();
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        Region? region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (region == null) return NotFound();

        var regionDto = new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl
        };
        return Ok(regionDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        var regionDomainModel = new Region()
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };

        await _dbContext.Regions.AddAsync(regionDomainModel);
        await _dbContext.SaveChangesAsync();

        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        Region? regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        if (regionDomainModel == null) return NotFound();

        regionDomainModel.Code = updateRegionRequestDto.Code;
        regionDomainModel.Name = updateRegionRequestDto.Name;
        regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
        await _dbContext.SaveChangesAsync();

        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl,
            Name = regionDomainModel.Name
        };

        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
    {
        Region? regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        if (regionDomainModel == null) return NotFound();
        _dbContext.Regions.Remove(regionDomainModel);
        await _dbContext.SaveChangesAsync();

        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        
        return Ok(regionDto);
    }
}