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

            string descriptionDocument = CheckCDocument();

            AddNotifications(new Contract<SupplierUpdateCommand>().Requires()
                                                     .IsNotNullOrEmpty(Name, nameof(Name), "Error. O nome do fornecedor está vazio ou é inexistente.")
                                                     .IsNotNullOrEmpty(Document, "Documento", "Erro. O documento do fornecedor está vazio ou é inexistente.")
                                                     .IsNullOrEmpty(descriptionDocument, "Documento", descriptionDocument)
                                                     .IsNotNull(IsActive, nameof(IsActive), "Error. O estado ativo é inexistente.")
                                                     );
        }

        private string CheckCDocument()
        {

            if (string.IsNullOrEmpty(Document))
                return "Erro. O documento do fornecedor está vazio ou é inexistente.";

            string description = Document.Length switch
            {
                1 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                2 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                3 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                4 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                5 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                6 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                7 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                8 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                9 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                10 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no mínimo 11 números.",
                12 => $"CPF está incorreto, há {Document.Length} números e é preciso ter no máximo 11 números.",
                13 => $"CNPJ está incorreto, há {Document.Length} números e é preciso ter no mínimo 14 números.",
                _ => ""
            };

            return description;
        }
    }
}
