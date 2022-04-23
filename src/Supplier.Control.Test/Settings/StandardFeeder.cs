
using Supplier.Control.Domain.Commands.Input.Suppliter;
using Supplier.Control.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Supplier.Control.Test.Settings
{
    public static class StandardFeeder
    {
        private static Guid Id = Guid.NewGuid();

        public static SupplierModel GetSupplier(bool documentRandon = false) => new()
        {
            Document = documentRandon ? "12312312312" : Random.Shared.Next().ToString(),
            Name = "New Supplier",
            IsActive = true
        };
        
        public static IQueryable<SupplierModel> GetSupplierQueryable(bool documentRandon = true) => new List<SupplierModel>
        {
            GetSupplier(documentRandon),
            GetSupplier(documentRandon),
            GetSupplier(documentRandon),
        }.AsQueryable();

        public static SupplierCreateCommand GetSupplierCreateCommand() => new()
        {
            Document = "43345234234212",
            Name = "New Supplier",
            IsActive = true
        };

        public static SupplierUpdateCommand GetSupplierUpdateCommand() => new()
        {
            Id = Id,
            Document = "43345234234212",
            Name = "New Supplier",
            IsActive = true
        };

        public static SupplierrRemoveCommand GetSupplierRemoveCommand() => new()
        {
            Id = Id,
            Document = "43345234234212",
            Name = "New Supplier",
            IsActive = true
        };
    }
}
