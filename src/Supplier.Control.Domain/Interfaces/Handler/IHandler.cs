
using Supplier.Control.Domain.Interfaces.Commands;

namespace Supplier.Control.Domain.Interfaces.Handler
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
