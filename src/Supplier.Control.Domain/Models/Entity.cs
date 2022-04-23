
using Supplier.Control.Domain.Interfaces.Models;

namespace Supplier.Control.Domain.Models
{
    public class Entity: IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
