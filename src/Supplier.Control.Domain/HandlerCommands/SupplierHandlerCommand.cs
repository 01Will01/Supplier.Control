
using Supplier.Control.Domain.Commands.Input.Suppliter;
using Supplier.Control.Domain.Commands.Output;
using Supplier.Control.Domain.Interfaces.Commands;
using Supplier.Control.Domain.Interfaces.Handler;
using Supplier.Control.Domain.Interfaces.Handler.Commands;
using Supplier.Control.Domain.Interfaces.Services;
using Supplier.Control.Domain.Models;
using Supplier.Control.Domain.Validators;

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
            command = CheckParamterValidator.CheckIsNotNull(command, nameof(command));
            command.Validate();

            if (!command.IsValid) return new GenericCommandResult(success: false, message: "Os dados estão com divergência.", command.Notifications);

            var supplier = new SupplierModel()
            {
                Name = command.Name,
                Document = command.Document,
                IsActive = command.IsActive
            };

            (bool hasCreated, string message) = await _supplierService.Create(supplier);

            return new GenericCommandResult(success: hasCreated, message: message, supplier);
        }

        public async Task<ICommandResult> Handle(SupplierUpdateCommand command)
        {
            command = CheckParamterValidator.CheckIsNotNull(command, nameof(command));
            command.Validate();

            if (!command.IsValid) return new GenericCommandResult(success: false, message: "Os dados estão com divergência", command.Notifications);

            var supplier = new SupplierModel()
            {
                Id = command.Id,
                Name = command.Name,
                Document = command.Document,
                IsActive = command.IsActive
            };

            (bool hasUpdate, string message) = await _supplierService.Update(supplier);

            return new GenericCommandResult(success: hasUpdate, message: message, supplier);
        }

        public async Task<ICommandResult> Handle(SupplierrRemoveCommand command)
        {
            command = CheckParamterValidator.CheckIsNotNull(command, nameof(command));
            command.Validate();

            if (!command.IsValid) return new GenericCommandResult(success: false, message: "Os dados estão com divergência.", command.Notifications);

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
