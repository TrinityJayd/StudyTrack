using DbManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var config = new ConfigurationBuilder().
        SetBasePath(Directory.GetCurrentDirectory()).
AddJsonFile("appsettings.json").
Build();

//Code Attribution
//Author: Anuraj 
//Link: https://dotnetthoughts.net/using-ef-core-in-a-separate-class-library/
//Code to facilitate the use of a separate class library for the database context
builder.Services.AddDbContext<Prog6212P2Context>(options =>
{
    options.UseSqlServer(config.GetConnectionString("UserDatabase"),
        assembly => assembly.MigrationsAssembly(typeof(Prog6212P2Context).Assembly.FullName));
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
