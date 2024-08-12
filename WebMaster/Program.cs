using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using WebMaster.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar la base de datos, en este caso SQL Server con Azure
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireSellerRole", policy => policy.RequireRole("Seller"));
    options.AddPolicy("RequireAccountantRole", policy => policy.RequireRole("Accountant"));
});

// Configuración de políticas de password
builder.Services.Configure<IdentityOptions>(options =>
{
    // Configurar opciones de password, lockout, etc.
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Configurar opciones de lockout
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configurar opciones de usuario
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddRazorPages();

var app = builder.Build();

// // Seed Roles and Users
// using var scope = app.Services.CreateScope();
// {
//     var services = scope.ServiceProvider;
//     var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//     var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//     await SeedRolesAndUsers(userManager, roleManager);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Autenticación
app.UseAuthorization();

app.MapRazorPages(); // Mapa de páginas de Identity
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run(); // La aplicación se ejecuta con el comando en consola dotnet run

// static async Task SeedRolesAndUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
// {
//     string[] roleNames = { "Admin", "Seller", "Accountant" };
//     IdentityResult roleResult;

//     foreach (var roleName in roleNames)
//     {
//         var roleExist = await roleManager.RoleExistsAsync(roleName);
//         if (!roleExist)
//         {
//             roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
//         }
//     }

//     // Create Admin user
//     var adminUser = new ApplicationUser
//     {
//         UserName = "admin@admin.com",
//         Email = "admin@admin.com"
//     };

//     string adminPassword = "Admin@123";
//     var user = await userManager.FindByEmailAsync("admin@admin.com");

//     if (user == null)
//     {
//         var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
//         if (createAdminUser.Succeeded)
//         {
//             await userManager.AddToRoleAsync(adminUser, "Admin");
//         }
//     }
// }
