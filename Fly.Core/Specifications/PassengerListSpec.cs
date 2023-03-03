using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications
{
    public class PassengerListSpec : Specification<Passenger>
    {
        public PassengerListSpec(PassengerParameter parameter, Page? page)
        {
            Query.Include(x => x.User);

            Query.Where(x => parameter.UserId == null || x.UserId == parameter.UserId);

            if (page != null)
            {
                Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).OrderByDescending(x => x.Id);
            }
        }
    }
}
