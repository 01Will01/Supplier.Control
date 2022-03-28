using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.Queries
{
    public interface ISupplierQuery
    {
        Task<List<SupplierModel>> GetAll();
        Task<SupplierModel> GetById(Guid? id);
        Task<SupplierModel> GetByDocument(string document);
    }
}
