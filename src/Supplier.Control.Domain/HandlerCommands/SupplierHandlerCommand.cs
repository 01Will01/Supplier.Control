
using Supplier.Control.Domain.Commands.Input.Suppliter;
using Supplier.Control.Domain.Commands.Output;
using Supplier.Control.Domain.Interfaces.Commands;
using Supplier.Control.Domain.Interfaces.Handler;
using Supplier.Control.Domain.Interfaces.Handler.Commands;
using Supplier.Control.Domain.Interfaces.Services;
using Supplier.Control.Domain.Models;

namespace Supplier.Control.Domain.HandlerCommands
{
    public class SupplierHandlerCommand : IHandler<SupplierUpdateCommand>, ISupplierHandlerCommand
    {
        private readonly ISupplierService _supplierService;

        public SupplierHandlerCommand(ISupplierService supplierService)
        {
            _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
        }

        public async Task<ICommandResult> Handle(SupplierCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid) return new GenericCommandResult(success: false, message: "Os dados estão com divergência.", command.Notifications);


            var supplier = new SupplierModel()
            {
                Name = command.Name,
                Document = command.Document,
                IsActive = command.IsActive
            };

            (SupplierModel currentSupplier, bool isExist, string message) = await _supplierService.Create(supplier);

            if (isExist)
                return new GenericCommandResult(success: isExist, message: message, currentSupplier);

            return new GenericCommandResult(success: isExist, message: message, supplier);
        }


        public async Task<ICommandResult> Handle(SupplierUpdateCommand command)
        {
            command.Validate();

            if (command.IsValid) return new GenericCommandResult(success: false, message: "Os dados estão com divergência", command.Notifications);


            var supplier = new SupplierModel()
            {
                Id = command.Id,
                Name = command.Name,
                Document = command.Document,
                IsActive = command.IsActive
            };

            bool status = await _supplierService.Updade(supplier);

            if (status)
                return new GenericCommandResult(success: false, message: "Atualização completa.", new { });

            return new GenericCommandResult(success: false, message: "Falha na etapa de atualização dos dados do fornecedor.", command.Notifications);
        }

        public async Task<ICommandResult> Handle(SupplierrRemoveCommand command)
        {
            command.Validate();

            if (command.IsValid) return new GenericCommandResult(success: false, message: "Os dados estão com divergência.", command.Notifications);


            var supplier = new SupplierModel()
            {
                Id = command.Id,
                Name = command.Name,
                Document = command.Document,
                IsActive = command.IsActive
            };

            bool wasExcluded = await _supplierService.Remove(supplier);

            if (wasExcluded)
                return new GenericCommandResult(success: wasExcluded, message: "Remoção completa.", supplier);

            return new GenericCommandResult(success: wasExcluded, message: "Falha na etapa de remoção dos dados do fornecedor.", supplier);
        }
    }
}
