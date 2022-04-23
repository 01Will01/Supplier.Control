using Supplier.Control.Domain.Models;

namespace Supplier.Control.Presentation.Models.Supplier
{
    public class SupplierOverviewViewModel
    {
        public List<SupplierModel> SupplierModels { get; set; }
        public SupplierModel SupplierModel { get; set; } = new SupplierModel();
        public bool HasResult { get; set; }
        public bool SuccessAction { get; set; }
        public string Message { get; set; }
    }
}
