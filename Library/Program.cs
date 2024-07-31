using Library.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "books",
    pattern: "Books/{action=Index}/{id?}",
    defaults: new { controller = "Books" });

app.MapControllerRoute(
    name: "authors",
    pattern: "Authors/{action=Index}/{id?}",
    defaults: new { controller = "Authors" });

app.MapControllerRoute(
    name: "members",
    pattern: "Members/{action=Index}/{id?}",
    defaults: new { controller = "Members" });

app.MapControllerRoute(
    name: "borrows",
    pattern: "Borrows/{action=Index}/{id?}",
    defaults: new { controller = "Borrows" });

app.Run();
