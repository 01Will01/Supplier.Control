using Supplier.Control.Domain.Models;

namespace Supplier.Control.Presentation.Models.Supplier
{
    public class SupplierRemoveViewModel
    {
        public SupplierModel SupplierModel { get; set; }
        public bool CanRemove { get; set; }
        public bool HasResult { get; set; }
        public bool SuccessAction { get; set; }
        public List<string> Message { get; set; } = new();
    }
}
