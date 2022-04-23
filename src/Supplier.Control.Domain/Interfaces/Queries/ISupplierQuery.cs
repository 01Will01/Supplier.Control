using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.Queries
{
    public interface ISupplierQuery
    {
        List<SupplierModel> GetAll();
        SupplierModel GetById(Guid? id);
        SupplierModel GetByDocument(string document);
    }
}
