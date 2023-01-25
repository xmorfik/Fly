using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class FlightService : IService<FlightDTO,PagedResponse<List<FlightDTO>>,FlightParameter>
{
    public readonly IRepository<Flight> _repository;
    public readonly IMapper _mapper;

    public FlightService(IRepository<Flight> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(FlightDTO item)
    {
        var entity = _mapper.Map<Flight>(item);
        await _repository.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Flight() { Id = id });
    }

    public async Task<Response<FlightDTO>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new FlightSpec(id, _mapper));
        if (result == null)
        {
            return new Response<FlightDTO>(new FlightDTO()) { Succeeded = false};
        }
        return new Response<FlightDTO>(result);
    }

    public async Task<PagedResponse<List<FlightDTO>>> GetListAsync(FlightParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new FlightListSpec(_mapper, parameter, page));
        var response = new PagedResponse<List<FlightDTO>>(items, page);
        return response;
    }

    public async Task UpdateAsync(FlightDTO item)
    {
        var entity = _mapper.Map<Flight>(item);
        await _repository.UpdateAsync(entity);  
    }
}