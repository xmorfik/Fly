using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class FlightService : IService<FlightDTO>, IFilter<FlightDTO, FlightParameter>
{
    public readonly IRepository<Flight> _repository;
    public readonly IMapper _mapper;

    public FlightService(IRepository<Flight> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task CreateAsync(FlightDTO item)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<FlightDTO>> FilterAsync(FlightParameter parameter)
    {
        return await _repository.ListAsync(new FlightListSpec(_mapper,parameter));
    }

    public async Task<FlightDTO> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new FlightSpec(id, _mapper));
        if (result == null)
        {
            return new FlightDTO();
        }
        return result;
    }

    public async Task<ICollection<FlightDTO>> GetListAsync()
    {
        return await _repository.ListAsync(new FlightListSpec(_mapper));
    }

    public Task UpdateAsync(FlightDTO item)
    {
        throw new NotImplementedException();
    }
}