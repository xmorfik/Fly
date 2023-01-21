using Fly.Core.Interfaces;

namespace Fly.Core.DataTransferObjects;

public abstract class BaseEntityDTO : IAggregateDTO
{ 
    public virtual int? Id { get; set; }
}