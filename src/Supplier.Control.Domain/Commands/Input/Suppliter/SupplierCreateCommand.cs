using Flunt.Notifications;
using Flunt.Validations;
using Supplier.Control.Domain.Interfaces.Commands;

namespace Supplier.Control.Domain.Commands.Input.Suppliter
{
    public class SupplierCreateCommand : Notifiable<Notification>, ICommand
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public bool IsActive { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<SupplierUpdateCommand>().Requires()
                                                     .IsNotNullOrEmpty(Name, nameof(Name), "Error. O nome do fornecedor está vazio ou é inexistente.")
                                                     .IsNotNullOrEmpty(Document, "Documento", "Erro. O documento do fornecedor está vazio ou é inexistente.")
                                                     .IsNotNull(IsActive, nameof(IsActive), "Error. O estado ativo é inexistente.")
                                                     );
        }
    }
}
