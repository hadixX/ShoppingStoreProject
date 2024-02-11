using Microsoft.EntityFrameworkCore;
using ShoppingStore.Data;
using Microsoft.AspNetCore.Identity;
using ShoppingStore.Repository;
using ShoppingStore.Repository.Interfaces;
using Microsoft.OpenApi.Models;
using ShoppingStore.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShoppingStoreDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingStoreConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShoppingStoreDBContext>();

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(5);
});
builder.Services.AddControllersWithViews();
//Extension for regestring our services
builder.Services.AddShopingServices();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
