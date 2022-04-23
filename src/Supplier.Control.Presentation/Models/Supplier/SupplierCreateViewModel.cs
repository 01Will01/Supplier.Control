using Supplier.Control.Domain.Models;

namespace Supplier.Control.Presentation.Models.Supplier
{
    public class SupplierCreateViewModel
    {
        public SupplierModel SupplierModel { get; set; }
        public bool CanCreate { get; set; }
        public bool HasResult { get; set; }
        public bool SuccessAction { get; set; }
        public List<string> Message { get; set; } = new();
    }
}
