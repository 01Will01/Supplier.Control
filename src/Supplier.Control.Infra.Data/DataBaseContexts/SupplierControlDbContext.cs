using Microsoft.EntityFrameworkCore;
using Supplier.Control.Domain.Models;
using Supplier.Control.Domain.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Supplier.Control.Domain.Interfaces.Models;
using Supplier.Control.Domain.Extensions;

namespace Supplier.Control.Infra.Data.DataBaseContexts
{
    public class SupplierControlDbContext : DbContext, ISupplierControlDbContext
    {
        public SupplierControlDbContext(DbContextOptions<SupplierControlDbContext> options) : base(options) { }

        public DbSet<SupplierModel> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DataBase");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public ValueTask<EntityEntry<SupplierModel>> AddAsync(SupplierModel supplier, CancellationToken cancellationToken = default) =>
            base.AddAsync(supplier, cancellationToken);

        public EntityEntry<SupplierModel> Remove(SupplierModel supplier) =>
            base.Remove(supplier);

        public EntityEntry<SupplierModel> Update(SupplierModel supplier) =>
            base.Update(supplier);

        public void DetachLocal<T>(T entity, Guid entryId) where T : class, IEntity
        {
            var local = base.Set<T>().Local.FirstOrDefault(entry => entry.Id.Equals(entryId));

            if (local.IsNotNull()) base.Entry(local).State = EntityState.Detached;

            base.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
