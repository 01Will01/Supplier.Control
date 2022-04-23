
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supplier.Control.Domain.Models
{
    public class SupplierModel : Entity
    {
        [Required]
        [Column(TypeName = "string")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "string")]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Required]
        [Column(TypeName = "bool")]
        [Display(Name = "Está ativo")]
        public bool IsActive { get; set; }
    }
}
