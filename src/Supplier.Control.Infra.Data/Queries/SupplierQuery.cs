
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SupplierModel>> GetAll() => await _dbContext?.Suppliers?.ToListAsync();

        public async Task<SupplierModel> GetById(Guid? id) => await _dbContext?.Suppliers?.FirstOrDefaultAsync(supplier => supplier.Id == id);

        public async Task<SupplierModel> GetByDocument(string document) => await _dbContext?.Suppliers?.FirstOrDefaultAsync(supplier => supplier.Document == document);
    }
}
