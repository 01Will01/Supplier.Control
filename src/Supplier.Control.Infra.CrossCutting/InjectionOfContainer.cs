
using Microsoft.Extensions.DependencyInjection;
using Supplier.Control.Domain.HandlerCommands;
using Supplier.Control.Domain.Interfaces.DbContext;
using Supplier.Control.Domain.Interfaces.Handler.Commands;
using Supplier.Control.Domain.Interfaces.Queries;
using Supplier.Control.Domain.Interfaces.Repositories;
using Supplier.Control.Domain.Interfaces.Services;
using Supplier.Control.Domain.Services;
using Supplier.Control.Infra.Data.DataBaseContexts;
using Supplier.Control.Infra.Data.Queries;
using Supplier.Control.Infra.Data.Repositories;

namespace Supplier.Control.Infra.CrossCutting
{
    public class InjectionOfContainer
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ISupplierControlDbContext, SupplierControlDbContext>();

            services.AddScoped<ISupplierQuery, SupplierQuery>();

            services.AddScoped<ISupplierHandlerCommand, SupplierHandlerCommand>();
            
            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<ISupplierRepositoty, SupplierRepositoty>();
            
        }
    }
}
