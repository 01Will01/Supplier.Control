using Moq;
using Supplier.Control.Domain.Interfaces.Repositories;
using Supplier.Control.Domain.Models;
using Supplier.Control.Domain.Services;
using Supplier.Control.Test.Settings;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Supplier.Control.Test.Services
{
    public class SupplierServiceTest
    {

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Constructor_Test(bool isSupplierRepositotyNull, bool exoectedArgumentNullException)
        {
            var _supplierRepositoty = isSupplierRepositotyNull ? null : new Mock<ISupplierRepositoty>().Object;

            if (exoectedArgumentNullException)
                Assert.Throws<ArgumentNullException>(() => new SupplierService(_supplierRepositoty));
            else
                Assert.NotNull(() => new SupplierService(_supplierRepositoty));
        }


        [Theory]
        [InlineData(true, false, false, true)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, false, false)]
        [InlineData(false, true, true, false)]
        public async Task Create_Test
        (bool isSupplierNull,
         bool isDocummentExist,
         bool isCreatedEntity,
         bool exoectedArgumentNullException)
        {
            var supplier = StandardFeeder.GetSupplier();
            Guid id = supplier.Id;

            var _supplierRepositoty = new Mock<ISupplierRepositoty>();

            _supplierRepositoty.Setup(_ => _.ExistByDocument(It.IsAny<string>())).Returns(isDocummentExist);
            _supplierRepositoty.Setup(_ => _.Create(It.IsAny<SupplierModel>())).ReturnsAsync((id, isCreatedEntity));
            _supplierRepositoty.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(supplier);

            supplier = isSupplierNull ? null : supplier;

            var _supplierService = new SupplierService(_supplierRepositoty.Object);

            if (exoectedArgumentNullException)
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await _supplierService.Create(supplier));
            else
            {
                var result = await _supplierService.Create(supplier);
                Assert.NotNull(result.Item1);
            }
        }


        [Theory]
        [InlineData(true, false, false, false, false, true)]
        [InlineData(false, true, false, false, false, false)]
        [InlineData(false, true, true, false, false, false)]
        [InlineData(false, false, false, false, false, false)]
        [InlineData(false, true, true, true, false, false)]
        [InlineData(false, true, true, false, true, false)]
        public async Task Update_Test
        (bool isSupplierNull,
         bool isNotGetSupplierNull,
         bool isSupplierCompareNotIqual,
         bool isDocummentExist,
         bool isUpdatedEntity,
         bool exoectedArgumentNullException)
        {
            var supplier = StandardFeeder.GetSupplier();

            var _supplierRepositoty = new Mock<ISupplierRepositoty>();

            if (isNotGetSupplierNull)
                _supplierRepositoty.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(supplier);

            if (isSupplierCompareNotIqual)
            {
                supplier.Id = Guid.NewGuid();
                supplier.Document = "32112332112";
                supplier.Name = "new name";
            }

            _supplierRepositoty.Setup(_ => _.ExistByDocument(It.IsAny<string>())).Returns(isDocummentExist);

            _supplierRepositoty.Setup(_ => _.Update(It.IsAny<SupplierModel>())).ReturnsAsync(isUpdatedEntity);

            supplier = isSupplierNull ? null : supplier;

            var _supplierService = new SupplierService(_supplierRepositoty.Object);

            if (exoectedArgumentNullException)
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await _supplierService.Update(supplier));
            else
            {
                var result = await _supplierService.Update(supplier);
                Assert.NotNull(result.Item2);
            }
        }


        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public async Task Remove_Test(bool isSupplierNull, bool exoectedArgumentNullException)
        {
            var supplier = isSupplierNull ? null : StandardFeeder.GetSupplier();

            var _supplierRepositoty = new Mock<ISupplierRepositoty>();

            var _supplierService = new SupplierService(_supplierRepositoty.Object);

            if (exoectedArgumentNullException)
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await _supplierService.Create(supplier));
            else
            {
                var result = await _supplierService.Create(supplier);
                Assert.NotNull(result.Item1);
            }
        }

    }
}
