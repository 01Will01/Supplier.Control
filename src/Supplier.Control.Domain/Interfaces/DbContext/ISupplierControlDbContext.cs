
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.DbContext
{
    public interface ISupplierControlDbContext
    {
        DbSet<SupplierModel> Suppliers { get; set; }

        ValueTask<EntityEntry<SupplierModel>> AddAsync(SupplierModel supplier, CancellationToken cancellationToken = default(CancellationToken));
        EntityEntry<SupplierModel> Remove(SupplierModel supplier);
        EntityEntry<SupplierModel> Update(SupplierModel supplier);
        Task SaveChangesAsync();
    }
}
