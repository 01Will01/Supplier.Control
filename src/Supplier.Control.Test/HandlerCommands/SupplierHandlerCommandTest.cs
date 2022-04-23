using Moq;
using Supplier.Control.Domain.HandlerCommands;
using Supplier.Control.Domain.Interfaces.Services;
using Supplier.Control.Domain.Models;
using Supplier.Control.Test.Settings;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Supplier.Control.Test.HandlerCommands
{
    public class SupplierHandlerCommandTest
    {
        private readonly SupplierHandlerCommand _commandHandler;
        private readonly ISupplierService _supplierService;

        public SupplierHandlerCommandTest()
        {
            var supplierService = new Mock<ISupplierService>();

            var response = (true, "Mensagem Test");
            bool responseRemove = true;

            supplierService.Setup(_ => _.Create(It.IsAny<SupplierModel>())).ReturnsAsync(response);
            supplierService.Setup(_ => _.Update(It.IsAny<SupplierModel>())).ReturnsAsync(response);
            supplierService.Setup(_ => _.Remove(It.IsAny<SupplierModel>())).ReturnsAsync(responseRemove);

            _supplierService = supplierService.Object;

            _commandHandler = new SupplierHandlerCommand(_supplierService);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Constructor_Test(bool isSupplierServiceNull, bool exoectedArgumentNullException)
        {
            var _supplierServiceMock = isSupplierServiceNull ? null : new Mock<ISupplierService>().Object;

            if (exoectedArgumentNullException)
                Assert.Throws<ArgumentNullException>(() => new SupplierHandlerCommand(_supplierServiceMock));
            else
                Assert.NotNull(() => new SupplierHandlerCommand(_supplierServiceMock));
        }

        [Fact]
        public async Task Create_Test()
        {
            var command = StandardFeeder.GetSupplierCreateCommand();
            var result = await _commandHandler.Handle(command);

            Assert.NotNull(result);

            command = null;
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commandHandler.Handle(command));
        }

        [Fact]
        public async Task Update_Test()
        {
            var command = StandardFeeder.GetSupplierUpdateCommand();
            var result = await _commandHandler.Handle(command);

            Assert.NotNull(result);

            command = null;
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commandHandler.Handle(command));
        }

        [Fact]
        public async Task Remove_Test()
        {
            var command = StandardFeeder.GetSupplierRemoveCommand();
            var result = await _commandHandler.Handle(command);

            Assert.NotNull(result);

            command = null;
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commandHandler.Handle(command));
        }
    }
}
