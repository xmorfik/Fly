using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications
{
    public class PassengerSpec : Specification<Passenger>, ISingleResultSpecification
    {
        public PassengerSpec(int id)
        {
            Query.Include(x => x.User);

            Query.Include(x => x.Tickets);

            Query.Where(x => x.Id == id);
        }

        public PassengerSpec(string id)
        {
            Query.Include(x => x.User);

            Query.Include(x => x.Tickets);

            Query.Where(x => x.UserId == id);
        }
    }
}
