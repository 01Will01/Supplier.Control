
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.Repositories
{
    public interface ISupplierRepositoty
    {
        Task<(Guid?, bool)> Create(SupplierModel newSupplier);
        Task<bool> Update(SupplierModel supplier);
        Task<bool> Remove(SupplierModel supplier);
        Task<List<SupplierModel>> GetAll();
        Task<bool> ExistByDocument(string document);
        Task<bool> ExistByName(string name);
        Task<bool> ExistById(Guid? id);
        Task<SupplierModel> GetById(Guid? id);
        Task<SupplierModel> GetByDocument(string document);
    }
}
