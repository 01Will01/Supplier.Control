#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplier.Control.Domain.Commands.Input.Suppliter;
using Supplier.Control.Domain.Commands.Output;
using Supplier.Control.Domain.Interfaces.Handler.Commands;
using Supplier.Control.Domain.Interfaces.Queries;
using Supplier.Control.Domain.Models;
using Supplier.Control.Presentation.Models.Supplier;

namespace Supplier.Control.Presentation.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierHandlerCommand _supplierHandlerCommand;
        private readonly ISupplierQuery _supplierQuery;

        public SupplierController(ISupplierHandlerCommand supplierHandlerCommand, ISupplierQuery supplierQuery)
        {
            _supplierHandlerCommand = supplierHandlerCommand;
            _supplierQuery = supplierQuery;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _supplierQuery.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id is null) return NotFound();

            var supplierModel = await _supplierQuery.GetById(id);

            if (supplierModel is null) return NotFound();

            return View(supplierModel);
        }

        public IActionResult Create()
        {
            var viewData = new SupplierCreateViewModel()
            {
                HasResult = false,
                CanCreate = true
            };

            return View(viewData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateViewModel supplierView)
        {

            var viewData = new SupplierCreateViewModel()
            {
                CanCreate = false,
                HasResult = true,
                Message = "Valores inválidos.",
                SuccessAction = false,
                SupplierModel = new SupplierModel()
            };

            if (ModelState.IsValid)
            {
                var command = new SupplierCreateCommand()
                {
                    Document = supplierView.SupplierModel.Document,
                    IsActive = supplierView.SupplierModel.IsActive,
                    Name = supplierView.SupplierModel.Name
                };

                var response = await _supplierHandlerCommand.Handle(command) as GenericCommandResult;

                viewData.Message = response.Message;
                viewData.SuccessAction = response.Success;

                if (response.Success)
                    viewData.SupplierModel = response.Data as SupplierModel;

                View(viewData);

                Thread.Sleep(7000);
                return RedirectToAction(nameof(Index));
            }

            return View(viewData);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null) return NotFound();

            var supplierModel = await _supplierQuery.GetById(id);

            var viewData = new SupplierUpdateViewModel()
            {
                CanUpdate = true,
                HasResult = false,
                SupplierModel = supplierModel
            };

            if (supplierModel is null) return NotFound();

            return View(viewData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierModel supplier)
        {
            if (id != supplier.Id) return NotFound();

            var viewData = new SupplierUpdateViewModel()
            {
                CanUpdate = false,
                HasResult = true,
                Message = "Dados não localizados.",
                SuccessAction = false,
                SupplierModel = supplier
            };

            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var command = new SupplierUpdateCommand()
                        {
                            Id = id,
                            Document = supplier.Document,
                            IsActive = supplier.IsActive,
                            Name = supplier.Name
                        };

                        var response = await _supplierHandlerCommand.Handle(command) as GenericCommandResult;

                        viewData.Message = response.Message;
                        viewData.SuccessAction = response.Success;

                        if (response.Success)
                            viewData.SupplierModel = await _supplierQuery.GetById(id);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierModelExists(supplier.Id)) return NotFound();

                    else throw;

                }

                View(viewData);

                Thread.Sleep(7000);
                return RedirectToAction(nameof(Index));
            }

            return View(viewData);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id is null) return NotFound();

            var supplier = await _supplierQuery.GetById(id);

            if (supplier is null) return NotFound();

            var viewData = new SupplierRemoveViewModel()
            {
                CanRemove = true,
                HasResult = false,
                SupplierModel = supplier
            };

            return View(viewData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await _supplierQuery.GetById(id);

            var command = new SupplierrRemoveCommand()
            {
                Id = id,
                Document = supplier.Document,
                IsActive = supplier.IsActive,
                Name = supplier.Name
            };

            var response = await _supplierHandlerCommand.Handle(command) as GenericCommandResult;

            var viewData = new SupplierRemoveViewModel()
            {
                CanRemove = false,
                HasResult = true,
                SuccessAction = response.Success,
                Message = response.Message,
                SupplierModel = supplier
            };

            return View(viewData); ;
        }

        private bool SupplierModelExists(Guid? id)
        {
            return _supplierQuery.GetById(id).Result != null;
        }
    }
}
