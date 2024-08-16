using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// ASP.NET knows about the MVC - it enables it
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:OnlineShopDbContextConnection"]);
});

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
app.MapRazorPages(); // Razor pages support

DbInitialiser.Seed(app);

// Start application
app.Run();