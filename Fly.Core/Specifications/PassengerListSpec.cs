using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Core.Specifications
{
    public class PassengerListSpec : Specification<Passenger>
    {
        public PassengerListSpec(PassengerParameter passengerParameter)
        {
            Query.Include(x => x.User);

            Query.Include(x => x.Tickets);
        }
    }
}
