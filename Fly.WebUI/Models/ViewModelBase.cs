using Fly.Core.Pagination;

namespace Fly.WebUI.Models;

public class ViewModelBase
{
    public MetaData MetaData { get; set; }
    public bool IsSelect { get; set; } = false;
    public string RedirectUri { get; set; }
}
