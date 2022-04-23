
using Microsoft.EntityFrameworkCore;
using Moq;
using Supplier.Control.Domain.Interfaces.DbContext;
using Supplier.Control.Domain.Models;
using Supplier.Control.Infra.Data.Queries;
using Supplier.Control.Test.Settings;
using System;
using System.Linq;
using Xunit;

namespace Supplier.Control.Test.Queries
{
    public class SupplierQueryTest
    {

        private readonly ISupplierControlDbContext _context;
        private readonly SupplierQuery _supplierQuery;
        private readonly SupplierModel _supplierModel;

        public SupplierQueryTest()
        {
            var context = new Mock<ISupplierControlDbContext>();
            var suppliers = StandardFeeder.GetSupplierQueryable();

            var dbSetSupplier = new Mock<DbSet<SupplierModel>>();

            dbSetSupplier.As<IQueryable<SupplierModel>>().Setup(m => m.Provider).Returns(suppliers.Provider);
            dbSetSupplier.As<IQueryable<SupplierModel>>().Setup(m => m.Expression).Returns(suppliers.Expression);
            dbSetSupplier.As<IQueryable<SupplierModel>>().Setup(m => m.ElementType).Returns(suppliers.ElementType);
            dbSetSupplier.As<IQueryable<SupplierModel>>().Setup(m => m.GetEnumerator()).Returns(suppliers.GetEnumerator());

            context.Setup(_ => _.Suppliers).Returns(dbSetSupplier.Object);

            _supplierModel = suppliers.FirstOrDefault();
            _context = context.Object;

            _supplierQuery = new SupplierQuery(_context);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Constructor_Test(bool isSupplierControlDbContextNull, bool expectedArgumentNullException)
        {
            var _supplierControlDbContext = isSupplierControlDbContextNull ? null : _context;

            if (expectedArgumentNullException)
                Assert.Throws<ArgumentNullException>(() => new SupplierQuery(_supplierControlDbContext));
            else
            {
                var instance = new SupplierQuery(_supplierControlDbContext);
                Assert.NotNull(instance);
            }
        }

        [Fact]
        public void GetAll_Test()
        {
            var result = _supplierQuery.GetAll;
            Assert.NotNull(result);
        }

        [Fact]
        public void ExistByName_Test()
        {
            var result = _supplierQuery.GetByDocument(_supplierModel.Document);
            Assert.NotNull(result);
        }

        [Fact]
        public void ExistById_Test()
        {
            var result = _supplierQuery.GetById(_supplierModel.Id);
            Assert.NotNull(result);
        }
    }
}
