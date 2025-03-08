using Achareh.Domain.Core.Contracts.Repositroy;
using Achareh.Domain.Core.Entities.User;
using Achareh.Infrastructure.EfCore.Common;
using Achareh.Infrastructure.EfCore.Repository;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.EntityFrameworkCore;
using Achareh.Domain.AppServices;
using Achareh.Domain.Core.Contracts.Service;
using Achareh.Domain.Services;
using Achareh.Domain.Core.Contracts.AppService;
using Achareh.Domain.Core.Contracts.Repositroy;
using Microsoft.AspNetCore.Http.Features;
using AChareh.Domain.Core.Contracts.AppService;
using AChareh.Domain.Core.Contracts.Service;



Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.CreateLogger();



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Host.ConfigureLogging(x =>
{
    x.ClearProviders();
    x.AddSerilog();
}).UseSerilog((context, config) =>
{
    config.WriteTo.Console();
    config.WriteTo.Seq("http://localhost:5341", apiKey: "Bd4vhPAlo0XnCylPydEQ");
});

builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.Console()
.WriteTo.Seq("http://localhost:5341", apiKey: "Bd4vhPAlo0XnCylPydEQ")
.Enrich.FromLogContext()
.CreateLogger();




builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICategoryRepositroy, CategoryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IExpertOfferRepository, ExpertOfferRepository>();
builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();


;

builder.Services.AddScoped<IHomeServiceService, HomeServiceService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IExpertOfferService, ExpertOfferService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICustomerExpertLoginService, CustomerExpertLoginService>();



//appservice
builder.Services.AddScoped<IHomeServiceAppService, HomeServiceAppService>();
builder.Services.AddScoped<IAdminAppService, AdminAppService>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<IExpertAppService, ExpertAppService>();
builder.Services.AddScoped<ICityAppService, CityAppService>();
builder.Services.AddScoped<ISubCategoryAppService, SubCategoryAppService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IExpertOfferAppService, ExpertOfferAppService>();
builder.Services.AddScoped<IRequestAppService, RequestAppService>();
builder.Services.AddScoped<IReviewAppService, ReviewAppService>();
builder.Services.AddScoped<ICustomerExpertLoginAppService, CustomerExpertLoginAppService>();




builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // ?????? ?? ???????
});



builder.Services.AddIdentity<User, IdentityRole<int>>
(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Admin/Login";
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

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "users",
    areaName: "Users",
    pattern: "Users/{controller=Home}/{action=Index}/{id?}");







app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


app.UseStaticFiles();



