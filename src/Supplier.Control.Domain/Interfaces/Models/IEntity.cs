using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplier.Control.Domain.Interfaces.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
