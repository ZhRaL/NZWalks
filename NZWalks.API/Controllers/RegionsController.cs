using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalkDbContext _dbContext;

    public RegionsController(NZWalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllRegions()
    {
        var regions = _dbContext.Regions.ToList();
        return Ok(regions);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetRegionById([FromRoute] Guid id)
    {
        Region? region = _dbContext.Regions.Find(id);
        if (region == null) return NotFound();
        return Ok(region);
    }
}