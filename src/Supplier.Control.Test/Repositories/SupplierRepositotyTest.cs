
using Microsoft.EntityFrameworkCore;
using Moq;
using Supplier.Control.Domain.Interfaces.DbContext;
using Supplier.Control.Domain.Models;
using Supplier.Control.Infra.Data.Repositories;
using Supplier.Control.Test.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Supplier.Control.Test.Repositories
{
    public class SupplierRepositotyTest
    {

        private readonly ISupplierControlDbContext _context;
        private readonly SupplierRepositoty _supplierRepositoty;
        private readonly SupplierModel _supplierModel;

        public SupplierRepositotyTest()
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
            _supplierRepositoty = new SupplierRepositoty(_context);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Constructor_Test(bool isSupplierControlDbContextNull, bool expectedArgumentNullException)
        {
            var _supplierControlDbContext = isSupplierControlDbContextNull ? null : _context;

            if (expectedArgumentNullException)
                Assert.Throws<ArgumentNullException>(() => new SupplierRepositoty(_supplierControlDbContext));
            else
            {
                var instance = new SupplierRepositoty(_supplierControlDbContext);
                Assert.NotNull(instance);
            }
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public async Task Create_Test(bool isSupplierNull, bool exoectedArgumentNullException)
        {
            var supplier = isSupplierNull ? null : StandardFeeder.GetSupplier();

            if (exoectedArgumentNullException)
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await _supplierRepositoty.Create(supplier));
            else
            {
                await _supplierRepositoty.Create(supplier);
                Assert.NotNull(supplier);
            }
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public async Task Update_Test(bool isSupplierNull, bool exoectedArgumentNullException)
        {
            var supplier = isSupplierNull ? null : _supplierModel;

            if (exoectedArgumentNullException)
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await _supplierRepositoty.Update(supplier));
            else
            {
                await _supplierRepositoty.Update(supplier);
                Assert.NotNull(supplier);
            }
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public async Task Remove_Test(bool isSupplierNull, bool exoectedArgumentNullException)
        {
            var supplier = isSupplierNull ? null : StandardFeeder.GetSupplier();

            if (exoectedArgumentNullException)
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await _supplierRepositoty.Remove(supplier));
            else
            {
                await _supplierRepositoty.Remove(supplier);
                Assert.NotNull(supplier);
            }

        }

        [Fact]
        public void ExistByDocument_Test()
        {
            bool status = _supplierRepositoty.ExistByDocument(_supplierModel.Document);
            Assert.True(status);
        }

        [Fact]
        public void ExistByName_Test()
        {
            bool status = _supplierRepositoty.ExistByName(_supplierModel.Name);
            Assert.True(status);
        }

        [Fact]
        public void ExistById_Test()
        {
            bool status = _supplierRepositoty.ExistById(_supplierModel.Id);
            Assert.True(status);
        }
    }
}
