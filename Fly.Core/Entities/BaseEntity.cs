using Finance.Core.Interfaces;

namespace Fly.Core.Entities;

public abstract class BaseEntity : IAggregateRoot
{
    public virtual int? Id { get; set; }
}