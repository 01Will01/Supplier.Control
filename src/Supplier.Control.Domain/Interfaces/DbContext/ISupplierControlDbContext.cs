
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Supplier.Control.Domain.Interfaces.Models;
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Interfaces.DbContext
{
    public interface ISupplierControlDbContext
    {
        DbSet<SupplierModel> Suppliers { get; set; }

        ValueTask<EntityEntry<SupplierModel>> AddAsync(SupplierModel supplier, CancellationToken cancellationToken = default);
        EntityEntry<SupplierModel> Remove(SupplierModel supplier);
        EntityEntry<SupplierModel> Update(SupplierModel supplier);
     
        /// <summary>
        /// Altera o estado anexo a entidade para ser modifica em um armazenamento em memória.
        /// </summary>
        /// <typeparam name="T">Generalizador de entidades.</typeparam>
        /// <param name="entity">O parametro da entidade.</param>
        /// <param name="entryId">Identidade da entidade.</param>
        void DetachLocal<T>(T entity, Guid entryId) where T : class, IEntity;
        Task SaveChangesAsync();
    }
}
