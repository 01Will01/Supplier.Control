
using Microsoft.EntityFrameworkCore;
using Supplier.Control.Domain.Interfaces.DbContext;
using Supplier.Control.Domain.Interfaces.Repositories;
using Supplier.Control.Domain.Models;
using Supplier.Control.Infra.Data.Queries;

namespace Supplier.Control.Infra.Data.Repositories
{
    public class SupplierRepositoty : SupplierQuery, ISupplierRepositoty
    {
        private readonly ISupplierControlDbContext _dbContext;
        public SupplierRepositoty(ISupplierControlDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<(Guid?, bool)> Create(SupplierModel newSupplier)
        {
            var result = _dbContext?.Suppliers?.AddAsync(newSupplier);

            await _dbContext?.SaveChangesAsync();

            bool isExist = Convert.ToBoolean((_dbContext?.Suppliers?.Any(x => x.Id == newSupplier.Id)));

            return (newSupplier.Id, isExist);
        }

        public async Task<bool> Update(SupplierModel supplier)
        {
            SupplierModel currentSupplier = _dbContext?.Suppliers.FirstOrDefault(x => x.Id == supplier.Id);

            _dbContext?.Update(supplier);

            await _dbContext?.SaveChangesAsync();

            SupplierModel changeSupplier = _dbContext?.Suppliers.FirstOrDefault(x => x.Id == supplier.Id);

            bool isChange = !changeSupplier.Equals(currentSupplier);

            return isChange;
        }

        public async Task<bool> Remove(SupplierModel supplier)
        {
            _dbContext?.Remove(supplier);

            await _dbContext?.SaveChangesAsync();

            bool isNotExist = !(await _dbContext?.Suppliers?.AnyAsync(x => x.Id == supplier.Id));

            return isNotExist;
        }

        public async Task<bool> ExistByDocument(string document) => await _dbContext?.Suppliers?.AnyAsync(supplier => supplier.Document == document);

        public async Task<bool> ExistByName(string name) => await _dbContext?.Suppliers?.AnyAsync(supplier => supplier.Name == name);

        public async Task<bool> ExistById(Guid? id) => await _dbContext?.Suppliers?.AnyAsync(supplier => supplier.Id == id);
    }
}
