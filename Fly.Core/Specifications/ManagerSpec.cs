using Ardalis.Specification;
using Fly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Core.Specifications;

public class ManagerSpec : Specification<Manager>, ISingleResultSpecification
{
    public ManagerSpec(int id)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
