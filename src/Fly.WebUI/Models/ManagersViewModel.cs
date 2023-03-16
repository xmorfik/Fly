using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class ManagersViewModel : ViewModelBase
{
    public ManagerParameter ManagerParameter { get; set; } = new();
    public List<Manager> PagedResponse { get; set; }

}
