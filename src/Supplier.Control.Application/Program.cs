using Supplier.Control.Domain.Interfaces.Queries;
using Supplier.Control.Infra.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

InjectionOfContainer.Configure(builder.Services);
SharedConfiguration.Configure(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/fornecedor", (
    ISupplierQuery _query) =>
    _query.GetAll())
    .WithName("GetSupplier")
    .WithTags("Supplier");

app.Run();