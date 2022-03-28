using Microsoft.EntityFrameworkCore;
using Supplier.Control.Domain.Models;
using Supplier.Control.Domain.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Supplier.Control.Infra.Data.DataBaseContexts
{
    public class SupplierControlDbContext : DbContext, ISupplierControlDbContext
    {
        public SupplierControlDbContext(DbContextOptions<SupplierControlDbContext> options) : base(options) { }

        public DbSet<SupplierModel> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DataBase");
        }

        public ValueTask<EntityEntry<SupplierModel>> AddAsync(SupplierModel supplier, CancellationToken cancellationToken = default(CancellationToken)) =>
            base.AddAsync(supplier, cancellationToken);

        public EntityEntry<SupplierModel> Remove(SupplierModel supplier) =>
            base.Remove(supplier);

        public EntityEntry<SupplierModel> Update(SupplierModel supplier) =>
            base.Update(supplier);

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
