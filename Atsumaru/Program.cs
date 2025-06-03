using Atsumaru.Data;
using Atsumaru.Models.Interface;
using Atsumaru.Models.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AtsumaruContextDB>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("AtsumaruContextDBConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>(CartRepository.GetCart);
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
//sesion
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
