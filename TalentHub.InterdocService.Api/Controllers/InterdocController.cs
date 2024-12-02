using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.InterdocService.Api.Models.Interdoc;
using TalentHub.InterdocService.Application.DTO.Interdoc;
using TalentHub.InterdocService.Application.Interfaces;

namespace TalentHub.InterdocService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterdocController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IInterdocService _service;
    
    public InterdocController(IMapper mapper, IInterdocService service)
    {
        _mapper = mapper;
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> SendInterdocAsync([FromBody] SendInterdocModel sendInterdocModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var interdoc = _mapper.Map<SendInterdocDto>(sendInterdocModel);
        
        if (interdoc is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.SendInterdocAsync(interdoc));
    }
}