using Supplier.Control.Infra.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

InjectionOfContainer.Configure(builder.Services);
SharedConfiguration.Configure(builder.Services);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
