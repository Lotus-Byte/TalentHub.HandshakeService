using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.HandshakeService.Api.Models.Handshake;
using TalentHub.HandshakeService.Application.DTO.Handshake;
using TalentHub.HandshakeService.Application.Interfaces;

namespace TalentHub.HandshakeService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HandshakeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IHandshakeService _service;

    public HandshakeController(IMapper mapper, IHandshakeService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> SendHandshakeAsync([FromBody] SendHandshakeModel sendHandshakeModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var handshake = _mapper.Map<SendHandshakeDto>(sendHandshakeModel);

        if (handshake is null) return BadRequest("Incorrect data");

        return Ok(await _service.SendHandshakeAsync(handshake));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHandshakesBySenderAsync(Guid fromUserId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var handshakesDto = await _service.GetHandshakesBySenderAsync (fromUserId);

        if (handshakesDto is null) return NotFound($"Employer with '{fromUserId}' id not found");

        return Ok(_mapper.Map<HandshakeDto[]>(handshakesDto));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHandshakeAsync(Guid handshakeId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.DeleteHandshakeAsync(handshakeId);

        return result ? NoContent() : NotFound();
    }
}