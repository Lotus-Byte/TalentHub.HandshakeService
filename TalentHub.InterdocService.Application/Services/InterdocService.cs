using AutoMapper;
using TalentHub.InterdocService.Application.DTO.Interdoc;
using TalentHub.InterdocService.Application.Interfaces;
using TalentHub.InterdocService.Infrastructure.Interfaces;
using TalentHub.InterdocService.Infrastructure.Models;

namespace TalentHub.InterdocService.Application.Services;

public class InterdocService : IInterdocService
{
    private readonly IMapper _mapper;
    private readonly IInterdocRepository _repository;

    public InterdocService(IInterdocRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<bool> SendInterdocAsync(SendInterdocDto createInterdocDto)
    {
        var interdoc = _mapper.Map<SendInterdocDto, Interdoc>(createInterdocDto); 
        
        return await _repository.AddInterdocAsync(interdoc);
    }

    public async Task<InterdocDto[]?> GetInterdocsBySenderAsync(Guid fromUserId)
    {
        var interdocs = await _repository.GetInterdocsBySenderAsync(fromUserId);
        
        if (interdocs == null) return null;
        
        var interdocsDto = _mapper.Map<Interdoc[], InterdocDto[]>(interdocs);
        
        return interdocsDto;
    }

    public async Task<bool> DeleteInterdocAsync(Guid interdocId)
    {
        return await _repository.DeleteInterdocAsync(interdocId);
    }
}