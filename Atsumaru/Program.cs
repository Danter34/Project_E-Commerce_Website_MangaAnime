using Atsumaru.Data;
using Atsumaru.Models.Interface;
using Atsumaru.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Đảm bảo dòng này đã có

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AtsumaruContextDB>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("AtsumaruContextDBConnection")));

// Cấu hình Identity (đã có)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // THÊM DÒNG NÀY ĐỂ IDENTITY BIẾT VỀ ROLES
    .AddEntityFrameworkStores<AtsumaruContextDB>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>(CartRepository.GetCart);
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

// Session (đã có)
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSession(); // (đã có)

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

// --- THÊM DÒNG NÀY ĐỂ ĐĂNG KÝ ADMIN AREA ROUTING ---
// ĐẶT TRƯỚC app.MapControllerRoute("default", ...)
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
// ----------------------------------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// --- THÊM PHẦN NÀY ĐỂ KHỞI TẠO ROLES VÀ ADMIN USER KHI ỨNG DỤNG KHỞI CHẠY ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AtsumaruContextDB>();
        // context.Database.Migrate(); // Nếu bạn muốn áp dụng migrations tự động khi khởi động

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedRolesAndAdminUser(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database seeding.");
    }
}
// -------------------------------------------------------------------------

app.Run();

// --- THÊM PHƯƠNG THỨC NÀY VÀO CUỐI FILE Program.cs (sau app.Run(); hoặc bất kỳ đâu bạn muốn đặt helper method) ---
async Task SeedRolesAndAdminUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // Tạo Role "Admin" và "User" nếu chưa có
    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Tạo tài khoản Admin nếu chưa có
    var adminEmail = "admin@atsumaru.com"; // Email cho tài khoản Admin
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true // Quan trọng: Xác nhận email để tài khoản có thể đăng nhập
        };
        // Mật khẩu mạnh hơn: Ví dụ "Admin@123!" hoặc "P@ssw0rd123"
        // Bạn nên đổi mật khẩu này thành một cái gì đó an toàn hơn trong môi trường thực tế!
        var createAdminResult = await userManager.CreateAsync(adminUser, "Admin@123");
        if (createAdminResult.Succeeded)
        {
            // Gán vai trò "Admin" cho tài khoản mới tạo
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
// ----------------------------------------------------------------------------------------------------------------