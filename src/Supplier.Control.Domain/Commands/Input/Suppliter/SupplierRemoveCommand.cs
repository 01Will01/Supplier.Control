using Flunt.Notifications;
using Flunt.Validations;
using Supplier.Control.Domain.Interfaces.Commands;

namespace Supplier.Control.Domain.Commands.Input.Suppliter
{
    public class SupplierrRemoveCommand : Notifiable<Notification>, ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public bool IsActive { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<SupplierUpdateCommand>().Requires()
                                                     .IsNotNull(Id, nameof(Id), "Erro. O valor de identificação é inexistente.")
                                                     .IsNotNullOrEmpty(Name, nameof(Name), "Error. O nome do fornecedor está vazio ou é inexistente.")
                                                     .IsNotNullOrEmpty(Document, "Documento", "Erro. O documento do fornecedor está vazio ou é inexistente.")
                                                     .IsNotNull(IsActive, nameof(IsActive), "Error. O estado ativo é inexistente.")
                                                     );
        }
    }
}
