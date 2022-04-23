
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<(bool, string)> Create(SupplierModel supplier);

        Task<(bool, string)> Update(SupplierModel supplier);

        Task<bool> Remove(SupplierModel supplier);
    }
}
