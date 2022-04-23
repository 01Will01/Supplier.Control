
using Supplier.Control.Domain.Extensions;
using Supplier.Control.Domain.Interfaces.Repositories;
using Supplier.Control.Domain.Interfaces.Services;
using Supplier.Control.Domain.Models;
using Supplier.Control.Domain.Validators;

namespace Supplier.Control.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepositoty _supplierRepositoty;

        public SupplierService(ISupplierRepositoty supplierRepositoty)
        {
            _supplierRepositoty = supplierRepositoty ?? throw new ArgumentNullException(nameof(supplierRepositoty));
        }

        public async Task<( bool, string)> Create(SupplierModel supplier)
        {
            supplier = CheckParamterValidator.CheckIsNotNull(supplier, nameof(supplier));

            bool isDocummentExist = _supplierRepositoty.ExistByDocument(supplier.Document);

            if (!isDocummentExist)
            {
                (Guid id, bool hasCreated) = await _supplierRepositoty.Create(supplier);

                if (hasCreated)
                    return ( hasCreated, "Criação completa.");
            }

            string description = !isDocummentExist
                                 ? "Falha na etapa de criação dos dados do fornecedor."
                                 : "O documento já existe na base de dados.";

            return (false, description);
        }

        public async Task<(bool, string)> Update(SupplierModel supplier)
        {
            supplier = CheckParamterValidator.CheckIsNotNull(supplier, nameof(supplier));

            string description = "Não foi localizado o fornecedor a partir da identificação única.";
            bool status = false;

            var supplierCurrent = _supplierRepositoty.GetById(supplier.Id);
            
            if (supplierCurrent is null)
                return (status, description);

            bool documentIsEqual = supplierCurrent.Document.Equals(supplier.Document);

            bool isAllowedUseSameDocument = documentIsEqual
                                            ? documentIsEqual
                                            : !_supplierRepositoty.ExistByDocument(supplier.Document);

            bool updatedEntity = supplier.IsNotIqual(supplierCurrent);

            if (isAllowedUseSameDocument && updatedEntity)
            {
                status = await _supplierRepositoty.Update(supplier);

                description = status
                                 ? "Atualização completa."
                                 : "Falha na etapa de atualização dos dados do fornecedor.";

                return (status, description);
            }

            status = false;
            description = !updatedEntity
                                ? "É preciso atualizar algum dado para executar essa funcionalidade."
                                : "O documento já existe na base de dados.";

            return (status, description);
        }

        public async Task<bool> Remove(SupplierModel supplier)
        {
            supplier = CheckParamterValidator.CheckIsNotNull(supplier, nameof(supplier));

            return await _supplierRepositoty.Remove(supplier);
        }
    }
}
