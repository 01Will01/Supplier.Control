using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Supplier.Control.Domain.Commands.Input.Suppliter;
using Supplier.Control.Domain.Commands.Output;
using Supplier.Control.Domain.Extensions;
using Supplier.Control.Domain.Interfaces.Handler.Commands;
using Supplier.Control.Domain.Interfaces.Queries;
using Supplier.Control.Domain.Models;
using Supplier.Control.Presentation.Models.Supplier;
using Supplier.Control.Shared;

namespace Supplier.Control.Presentation.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierHandlerCommand _supplierHandlerCommand;
        private readonly ISupplierQuery _supplierQuery;
        private readonly IMemoryCache _memoryCache;

        public SupplierController(ISupplierHandlerCommand supplierHandlerCommand, ISupplierQuery supplierQuery, IMemoryCache memoryCache)
        {
            _supplierHandlerCommand = supplierHandlerCommand ?? throw new ArgumentNullException(nameof(supplierHandlerCommand));
            _supplierQuery = supplierQuery ?? throw new ArgumentNullException(nameof(supplierQuery));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public IActionResult Index()
        {
            var suppliers = _supplierQuery.GetAll();

            suppliers?.ForEach(supplier => supplier.Document = supplier.Document.Length is 11 ? supplier.Document.PatterCPF() : supplier.Document.PatterCNPJ());
            var view = new SupplierOverviewViewModel()
            {
                SupplierModels = suppliers,
                HasResult = CheckMessage(Settings.KEYMESSAGE, out var result),
                Message = result?.Message,
                SuccessAction = Convert.ToBoolean(result?.Success)
            };

            return View(view);
        }

        public IActionResult Details(Guid? id)
        {
            if (id is null) return NotFound();

            var supplier = _supplierQuery.GetById(id);

            if (supplier is null) return NotFound();

            supplier.Document = supplier.Document.Length is 11 ? supplier.Document.PatterCPF() : supplier.Document.PatterCNPJ();

            return View(supplier);
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
                CanCreate = true,
                HasResult = true,
                Message = "Valores inválidos.".TransformInList(),
                SuccessAction = false,
                SupplierModel = new SupplierModel()
            };

            if (!ModelState.IsValid) return View(viewData);

            var command = new SupplierCreateCommand()
            {
                Document = supplierView.SupplierModel.Document.StandardizeSeparator(string.Empty),
                IsActive = supplierView.SupplierModel.IsActive,
                Name = supplierView.SupplierModel.Name
            };

            var response = await _supplierHandlerCommand.Handle(command) as GenericCommandResult;

            viewData.SuccessAction = Convert.ToBoolean(response?.Success);

            if (viewData.SuccessAction)
            {
                viewData.SupplierModel = supplierView.SupplierModel;

                IncludeMessage(Settings.KEYMESSAGE, response);
                return RedirectToAction(nameof(Index));
            }

            if (response != null)
            {
                viewData.Message.Add(response.Message);

                List<Notification> notifications = response.Data as List<Notification>;

                notifications?.ForEach(notification =>
                {
                    viewData.Message.Add(notification.Message);
                });

            }

            return View(viewData);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id is null) return NotFound();

            var supplier = _supplierQuery.GetById(id);

            if (supplier is null) return NotFound();

            supplier.Document = supplier.Document.Length is 11 ? supplier.Document.PatterCPF() : supplier.Document.PatterCNPJ();

            var viewData = new SupplierUpdateViewModel()
            {
                CanUpdate = true,
                HasResult = false,
                SupplierModel = supplier
            };

            return View(viewData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierUpdateViewModel supplierView)
        {
            if (id != supplierView.SupplierModel.Id) return NotFound();

            var viewData = new SupplierUpdateViewModel()
            {
                CanUpdate = true,
                HasResult = true,
                Message = "Dados não localizados.".TransformInList(),
                SuccessAction = false,
                SupplierModel = supplierView.SupplierModel
            };

            if (!ModelState.IsValid) return View(viewData);

            var command = new SupplierUpdateCommand()
            {
                Id = id,
                Document = supplierView.SupplierModel.Document.StandardizeSeparator(string.Empty),
                IsActive = supplierView.SupplierModel.IsActive,
                Name = supplierView.SupplierModel.Name
            };

            var response = await _supplierHandlerCommand.Handle(command) as GenericCommandResult;

            viewData.SuccessAction = Convert.ToBoolean(response?.Success);

            if (viewData.SuccessAction)
            {
                IncludeMessage(Settings.KEYMESSAGE, response);
                return RedirectToAction(nameof(Index));
            }

            if (response != null)
            {
                viewData.Message.Add(response.Message);

                List<Notification> notifications = response.Data as List<Notification>;

                notifications?.ForEach(notification =>
                {
                    viewData.Message.Add(notification.Message);
                });

            }

            return View(viewData);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id is null) return NotFound();

            var supplier = _supplierQuery.GetById(id);

            if (supplier is null) return NotFound();

            supplier.Document = supplier.Document.Length is 11 ? supplier.Document.PatterCPF() : supplier.Document.PatterCNPJ();

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
            var supplier = _supplierQuery.GetById(id);

            if (supplier is null) return NoContent();

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
                CanRemove = true,
                HasResult = true,
                SuccessAction = Convert.ToBoolean(response?.Success),
                Message = response?.Message.TransformInList() ?? "Falha no processo.".TransformInList(),
                SupplierModel = supplier
            };

            if (viewData.SuccessAction)
            {
                IncludeMessage(Settings.KEYMESSAGE, response);
                return RedirectToAction(nameof(Index));
            }

            if (response != null)
            {
                viewData.Message.Add(response.Message);

                List<Notification> notifications = response.Data as List<Notification>;

                notifications?.ForEach(notification =>
                {
                    viewData.Message.Add(notification.Message);
                });

            }

            return View(viewData);
        }

        private void IncludeMessage(string key, GenericCommandResult result)
        {
            _memoryCache.Set(
                            key,
                            result,
                            new MemoryCacheEntryOptions()
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1800),
                                SlidingExpiration = TimeSpan.FromSeconds(150)
                            });
        }

        private bool CheckMessage(string key, out GenericCommandResult result)
        {
            bool status = _memoryCache.TryGetValue(key, out result);

            if (status)
                _memoryCache.Remove(key);

            return status;
        }
    }
}
