using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using OnlineShop.App;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// ASP.NET knows about the MVC - it enables it
builder.Services.AddControllersWithViews().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddRazorPages();
//builder.Services.AddRazorComponents().AddInteractiveServerComponents(); // Blazor support
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:OnlineShopDbContextConnection"]);
});

// builder.Services.AddControllers(); // Used for building APIs

// Application instance is ready
var app = builder.Build();

// Middleware added (order matters)
// Handles wwwroot file static details
app.UseStaticFiles();
// Support for sessions
app.UseSession();

if (app.Environment.IsDevelopment())
{
    // Error message for developers
    app.UseDeveloperExceptionPage();
}

// Endpoint middleware (at the end) - handle MVC controls on route
// Default controller route = {controller=Home}/{action=Index}/{id?}
app.MapDefaultControllerRoute();
//app.UseAntiforgery(); // Blazor request
// app.MapControllers(); // Used for building APIs
app.MapRazorPages(); // Razor pages support
//app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); // Ineractive server mode for Blazor

DbInitialiser.Seed(app);

// Start application
app.Run();