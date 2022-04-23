
using Microsoft.EntityFrameworkCore;
using Supplier.Control.Domain.Interfaces.DbContext;
using Supplier.Control.Domain.Interfaces.Repositories;
using Supplier.Control.Domain.Models;
using Supplier.Control.Domain.Validators;
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

        public async Task<(Guid, bool)> Create(SupplierModel newSupplier)
        {
            newSupplier = CheckParamterValidator.CheckIsNotNull(newSupplier, nameof(newSupplier));
            _dbContext.Suppliers.Add(newSupplier);

            await _dbContext.SaveChangesAsync();

            bool hasCreated = ExistById(newSupplier.Id);

            return (newSupplier.Id, hasCreated);
        }

        public async Task<bool> Update(SupplierModel supplier)
        {
            supplier = CheckParamterValidator.CheckIsNotNull(supplier, nameof(supplier));

            SupplierModel currentSupplier = _dbContext.Suppliers.FirstOrDefault(x => x.Id == supplier.Id);

            _dbContext.DetachLocal(supplier, supplier.Id);

            _dbContext.Suppliers.Update(supplier);

            await _dbContext.SaveChangesAsync();

            SupplierModel changeSupplier = base.GetById(supplier.Id);

            return Convert.ToBoolean(!changeSupplier.Equals(currentSupplier ?? new()));
        }

        public async Task<bool> Remove(SupplierModel supplier)
        {
            supplier = CheckParamterValidator.CheckIsNotNull(supplier, nameof(supplier));

            _dbContext.DetachLocal(supplier, supplier.Id);

            _dbContext.Suppliers.Remove(supplier);

            await _dbContext.SaveChangesAsync();

            return !(ExistById(supplier.Id));
        }

        public bool ExistByDocument(string document) => _dbContext.Suppliers.Any(supplier => supplier.Document == document);

        public bool ExistByName(string name) => _dbContext.Suppliers.Any(supplier => supplier.Name == name);

        public bool ExistById(Guid? id) => _dbContext.Suppliers.Any(supplier => supplier.Id == id);
    }
}
