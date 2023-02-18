using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Core.Interfaces;

public interface ITrackingService
{
    public Task Track(int id);
}
