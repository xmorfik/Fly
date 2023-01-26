using Fly.Core.Interfaces;

namespace Fly.Core.Entities;

public abstract class BaseEntity : IAggregateEntities
{
    public virtual int? Id { get; set; }
}