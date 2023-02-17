using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class ManagersViewModel
{
    public MetaData MetaData { get; set; }
    public ManagerParameter ManagerParameter { get; set; } = new();
    public PagedResponse<Manager> PagedResponse { get; set; }
}
