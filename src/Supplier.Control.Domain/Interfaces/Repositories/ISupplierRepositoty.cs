
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.Repositories
{
    public interface ISupplierRepositoty
    {
        Task<(Guid, bool)> Create(SupplierModel newSupplier);
        Task<bool> Update(SupplierModel supplier);
        Task<bool> Remove(SupplierModel supplier);
        List<SupplierModel> GetAll();
        bool ExistByDocument(string document);
        bool ExistByName(string name);
        bool ExistById(Guid? id);
        SupplierModel GetById(Guid? id);
        SupplierModel GetByDocument(string document);
    }
}
