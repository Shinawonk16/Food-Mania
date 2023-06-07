using System.Net.Mime;
using Food_Mania.Models.ApplicationContext;
using Food_Mania.Models.Repository.Implementation;
using Food_Mania.Models.Repository.Interface;
using Food_Mania.Models.Service.Implementation;
using Food_Mania.Models.Service.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Repository.Implementation;
using Models.Repository.Interface;
using Models.Service.Implementation;
using Models.Service.Interface;
using UploadImages;
using static System.Net.Mime.MediaTypeNames;
using static UploadImages.Upload;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("ManiaConnectionString");
builder.Services.AddDbContext<Context>(option => option.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
// builder.Services.AddDbContext<Context>(options => options
//            .UseMySql(ServerVersion(Auto).GetConnectionString("ManiaConnection")));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();

builder.Services.AddScoped<IFoodImage,Upload>();

builder.Services.AddScoped<IFoodOrderRepository, FoodOrderRepository>();


builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(option =>
 {
    option.Cookie.Name = "FoodManiaCookie";
    option.LoginPath = "/User/Login";
    option.LogoutPath = "/User/Logout";
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
