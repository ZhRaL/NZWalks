using AutoMapper;
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
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionsController(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRegions()
    {
        var regions = await _regionRepository.GetAllAsync();
        
        var regionDto = _mapper.Map<List<RegionDto>>(regions);
        return Ok(regionDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        Region? region = await _regionRepository.GetByIdAsync(id);
        if (region == null) return NotFound();

        
        RegionDto? regionDto = _mapper.Map<RegionDto>(region);
        return Ok(regionDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

        regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // Map DTO to Domain Model
        var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

        regionDomainModel = await _regionRepository.UpdateAsync(id,regionDomainModel);
        if (regionDomainModel == null) return NotFound();

        RegionDto? regionDto = _mapper.Map<RegionDto>(regionDomainModel);
        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
    {
        Region? regionDomainModel = await _regionRepository.DeleteAsync(id);
        if (regionDomainModel == null) return NotFound();

        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
        
        return Ok(regionDto);
    }
}