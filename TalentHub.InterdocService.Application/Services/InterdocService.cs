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
    
    public async Task<bool> SendInterdocAsync(SendInterdocDto sendInterdocDto)
    {
        var interdoc = _mapper.Map<SendInterdocDto, Interdoc>(sendInterdocDto); 
        
        await _repository.SendInterdocAsync(interdoc);
    }

    public async Task<IEnumerable<InterdocDto>?> GetInterdocsByFromUserIdAsync(Guid fromUserId)
    {
        var interdocs = await _repository.GetInterdocsByFromUserIdAsync(fromUserId);
        
        if (interdocs == null) return null;
        
        var interdocsDto = _mapper.Map<IEnumerable<Interdoc>, IEnumerable<InterdocDto>>(interdocs);
        
        return interdocsDto;
    }

    public async Task<bool> DeleteInterdocsByToUserIdAsync(Guid toUserId)
    {
        return await _repository.DeleteInterdocsByToUserIdAsync(toUserId);
    }
}