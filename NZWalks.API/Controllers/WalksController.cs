using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _repository;

    public WalksController(IMapper mapper, IWalkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);
        await _repository.CreateAsync(walkDomainModel);
        
        var walk = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walk);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var walks = await _repository.GetAllAsync();
        
        return Ok(_mapper.Map<List<WalkDto>>(walks));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var walk = await _repository.GetByIdAsync(id);
        if(walk == null) return NotFound();
        
        return Ok(_mapper.Map<WalkDto>(walk));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {
        var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
        Walk? updatedWalk = await _repository.UpdateAsync(id, walkDomainModel);
        if(updatedWalk == null) return NotFound();
        return Ok(_mapper.Map<WalkDto>(updatedWalk));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Walk? deletedWalk = await _repository.DeleteAsync(id);
        if(deletedWalk == null) return NotFound();
        return Ok(deletedWalk);
    }
}