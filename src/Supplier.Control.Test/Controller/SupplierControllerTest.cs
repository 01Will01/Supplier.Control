using Microsoft.Extensions.Caching.Memory;
using Moq;
using Supplier.Control.Domain.Extensions;
using Supplier.Control.Domain.Interfaces.Handler.Commands;
using Supplier.Control.Domain.Interfaces.Queries;
using Supplier.Control.Domain.Models;
using Supplier.Control.Presentation.Controllers;
using Supplier.Control.Presentation.Models.Supplier;
using Supplier.Control.Test.Settings;
using System;
using System.Linq;
using Xunit;

namespace Supplier.Control.Test.Controller
{
    public class SupplierControllerTest
    {

        private readonly ISupplierHandlerCommand _supplierHandlerCommand;
        private readonly ISupplierQuery _supplierQuery;
        private readonly IMemoryCache _memoryCache;
        private readonly SupplierController _supplierController;
        private readonly SupplierModel _supplierModel;

        public SupplierControllerTest()
        {
            _supplierModel = StandardFeeder.GetSupplier();
            var supplierHandlerCommandMock = new Mock<ISupplierHandlerCommand>();
            var supplierQueryMock = new Mock<ISupplierQuery>();
            var memoryCacheMock = new Mock<IMemoryCache>();

            var suppliers = StandardFeeder.GetSupplierQueryable().ToList();
            supplierQueryMock.Setup(_ => _.GetAll()).Returns(suppliers);
            supplierQueryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(suppliers.Last());
            supplierQueryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(suppliers.Last());
            _supplierHandlerCommand = supplierHandlerCommandMock.Object;
            _supplierQuery = supplierQueryMock.Object;
            _memoryCache = memoryCacheMock.Object;

            _supplierController = new SupplierController(_supplierHandlerCommand, _supplierQuery, _memoryCache);
        }


        [Theory]
        [InlineData(true, false, false, false)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, false)]
        public void Constructor_Test(bool isSupplierHandlerCommandNull, bool isSupplierQueryNull, bool isMemoryCacheNull, bool expectedArgumentNullException)
        {
            var _supplierHandlerCommandMock = isSupplierHandlerCommandNull ? null : new Mock<ISupplierHandlerCommand>().Object;
            var _supplierQueryMock = isSupplierQueryNull ? null : new Mock<ISupplierQuery>().Object;
            var _memoryCacheMock = isMemoryCacheNull ? null : new Mock<IMemoryCache>().Object;

            if (expectedArgumentNullException)
                Assert.Throws<ArgumentNullException>(() => new SupplierController(_supplierHandlerCommandMock, _supplierQueryMock, _memoryCacheMock));
            else
                Assert.NotNull(() => new SupplierController(_supplierHandlerCommandMock, _supplierQueryMock, _memoryCacheMock));
        }

        [Fact]
        public void Index_Test()
        {
            var result = _supplierController.Index();
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Details_Test(bool isIdNull)
        {
            Guid? id = isIdNull ? null : _supplierModel.Id;
            var result = _supplierController.Details(id);
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Test()
        {
            var result = _supplierController.Create();
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Post_Test()
        {
            var model = new SupplierCreateViewModel()
            {
                CanCreate = false,
                HasResult = true,
                Message = "Test".TransformInList(),
                SuccessAction = false,
                SupplierModel = _supplierModel,
            };

            var result = _supplierController.Create(model);
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_Test()
        {
            Guid id = _supplierModel.Id;

            var result = _supplierController.Edit(id);
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_Post_Test()
        {
            Guid id = _supplierModel.Id;

            var model = new SupplierUpdateViewModel()
            {
                CanUpdate = false,
                HasResult = false,
                Message = "Test".TransformInList(),
                SuccessAction = false,
                SupplierModel = _supplierModel,
            };

            var result = _supplierController.Edit(id, model);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Delete_Test(bool isIdNull)
        {
            Guid? id = isIdNull ? null : _supplierModel.Id;

            var result = _supplierController.Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_Post_Test()
        {
            Guid id = _supplierModel.Id;

            var result = _supplierController.DeleteConfirmed(id);
            Assert.NotNull(result);
        }
    }
}
