﻿using Supplier.Control.Domain.Models;

namespace Supplier.Control.Presentation.Models.Supplier
{
    public class SupplierUpdateViewModel
    {
        public SupplierModel SupplierModel { get; set; }
        public bool CanUpdate { get; set; }
        public bool HasResult { get; set; }
        public bool SuccessAction { get; set; }
        public List<string> Message { get; set; } = new();
    }
}
