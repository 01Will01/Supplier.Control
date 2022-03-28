
using Supplier.Control.Domain.Interfaces.Repositories;
using Supplier.Control.Domain.Interfaces.Services;
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepositoty _supplierRepositoty;

        public SupplierService(ISupplierRepositoty supplierRepositoty)
        {
            _supplierRepositoty = supplierRepositoty;
        }

        public async Task<(SupplierModel, bool, string)> Create(SupplierModel supplier)
        {
            (Guid? id, bool isExist) = await _supplierRepositoty.Create(supplier);

            if (isExist)
            {
                var curretSupplier = await _supplierRepositoty.GetById(id);
                return (curretSupplier, isExist, "Criação completa.");
            }
            else
                return (supplier, isExist, "Falha na etapa de criação dos dados do fornecedor.");
        }

        public async Task<bool> Updade(SupplierModel supplier) => await _supplierRepositoty.Update(supplier);

        public async Task<bool> Remove(SupplierModel supplier) => await _supplierRepositoty.Remove(supplier);
    }
}
