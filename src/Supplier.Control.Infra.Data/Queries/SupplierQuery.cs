using Supplier.Control.Domain.Interfaces.DbContext;
using Supplier.Control.Domain.Interfaces.Queries;
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Infra.Data.Queries
{
    public class SupplierQuery : ISupplierQuery
    {
        private readonly ISupplierControlDbContext _dbContext;
        public SupplierQuery(ISupplierControlDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<SupplierModel> GetAll() => _dbContext?.Suppliers?.ToList();

        public SupplierModel GetById(Guid? id) => _dbContext?.Suppliers?.FirstOrDefault(supplier => supplier.Id == id);

        public SupplierModel GetByDocument(string document) => _dbContext?.Suppliers?.FirstOrDefault(supplier => supplier.Document == document);
    }
}
