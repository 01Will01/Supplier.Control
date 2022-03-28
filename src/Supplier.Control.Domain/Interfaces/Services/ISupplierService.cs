
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<(SupplierModel, bool, string)> Create(SupplierModel supplier);

        Task<bool> Updade(SupplierModel supplier);

        Task<bool> Remove(SupplierModel supplier);
    }
}
