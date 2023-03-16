using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;

namespace Fly.WebUI.Services;

public class SeatsGeneratorService : ISeatsGeneratorService<SeatsDto>
{
    private readonly IService<Seat, SeatParameter> _service;
    public SeatsGeneratorService(IService<Seat, SeatParameter> service)
    {
        _service = service;
    }

    public async Task Generate(SeatsDto seats)
    {
        for (int i = 1; i <= seats.EconomClass; i++)
        {
            var item = new Seat
            {
                AircraftId = seats.AircraftId,
                SeatClass = SeatClass.EconomClass,
                Row = 1,
                Column = i,
            };
            await _service.CreateAsync(item);
        }

        for (int i = 1; i <= seats.FirstClass; i++)
        {
            var item = new Seat
            {
                AircraftId = seats.AircraftId,
                SeatClass = SeatClass.FirstClass,
                Row = 2,
                Column = i,
            };
            await _service.CreateAsync(item);
        }

        for (int i = 1; i <= seats.BusinessClass; i++)
        {
            var item = new Seat
            {
                AircraftId = seats.AircraftId,
                SeatClass = SeatClass.BusinessClass,
                Row = 3,
                Column = i,
            };
            await _service.CreateAsync(item);
        }
    }
}
