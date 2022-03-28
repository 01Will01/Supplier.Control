
using Supplier.Control.Domain.Interfaces.Commands;

namespace Supplier.Control.Domain.Commands.Output
{
    public class GenericCommandResult : ICommandResult
    {
        public GenericCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; private set; }

        public string Message { get; private set; }

        public object Data
        {
            get; private set;
        }
    }
}
