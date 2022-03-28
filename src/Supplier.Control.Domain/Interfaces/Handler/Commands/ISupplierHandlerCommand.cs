
using Supplier.Control.Domain.Commands.Input.Suppliter;
using Supplier.Control.Domain.Interfaces.Commands;

namespace Supplier.Control.Domain.Interfaces.Handler.Commands
{
    public interface ISupplierHandlerCommand
    {
        Task<ICommandResult> Handle(SupplierCreateCommand command);

        Task<ICommandResult> Handle(SupplierUpdateCommand command);

        Task<ICommandResult> Handle(SupplierrRemoveCommand command);
    }
}
