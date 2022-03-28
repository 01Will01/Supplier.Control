
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Supplier.Control.Infra.Data.DataBaseContexts;

namespace Supplier.Control.Infra.CrossCutting
{
    public static class SharedConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddDbContext<SupplierControlDbContext>(opt => opt.UseInMemoryDatabase("DataBase"));
        }
    }
}
