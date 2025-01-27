using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.HandshakeService.Api.Models.Application;
using TalentHub.HandshakeService.App.DTO.Application;
using TalentHub.HandshakeService.App.Interfaces;

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
    public async Task<IActionResult> SendApplicationAsync([FromBody] SendApplicationModel sendApplicationModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var application = _mapper.Map<SendApplicationDto>(sendApplicationModel);

        if (application is null) return BadRequest("Incorrect data");

        return Ok(await _service.SendApplicationAsync(application));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetApplicationsBySenderAsync(Guid fromUserId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var applicationsDto = await _service.GetApplicationsBySenderAsync (fromUserId);

        if (applicationsDto is null) return NotFound($"Employer with '{fromUserId}' id not found");

        return Ok(_mapper.Map<ApplicationDto[]>(applicationsDto));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteApplicationAsync(Guid applicationId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.DeleteApplicationAsync(applicationId);

        return result ? NoContent() : NotFound();
    }
}