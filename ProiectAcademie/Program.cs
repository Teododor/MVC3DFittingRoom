using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation;
using Proiect.BusinessLogic.Implementation.City;
using Proiect.BusinessLogic.Implementation.Comment;
using Proiect.BusinessLogic.Implementation.Countries;
using Proiect.BusinessLogic.Implementation.FavoriteProducts;
using Proiect.BusinessLogic.Implementation.Image;
using Proiect.BusinessLogic.Implementation.Implementation;
using Proiect.BusinessLogic.Implementation.Product;
using Proiect.BusinessLogic.Implementation.UserBasket;
using Proiect.BusinessLogic.Implementation.UserRating;
using Proiect.Common.DTOs;
using Proiect.DataAccess;
using Proiect.Entities;
using Proiect.WebApp.Code.Base;
using ProiectAcademie.Code.ExtensionMethods;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register DbContext and UnitOfWork (adapt the connection string as needed)
string connectionString = "Server=TAXINTE\\SQLEXPRESS;Initial Catalog=dbProiectATI;Integrated Security=true;TrustServerCertificate=true;";
builder.Services.AddDbContext<DbProiectAtiContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCurrentUser();

// Register ServiceDependencies
builder.Services.AddScoped<ServiceDependencies>(provider =>
{
    var mapper = provider.GetRequiredService<IMapper>();
    var unitOfWork = provider.GetRequiredService<UnitOfWork>();
    var currentUserDto = provider.GetRequiredService<CurrentUserDto>();
    return new ServiceDependencies(mapper, unitOfWork, currentUserDto);
});


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ControllerDependencies>();

builder.Services.AddScoped<UserAccountService>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<CommentService>();

builder.Services.AddScoped<FavoriteProductsService>();

builder.Services.AddScoped<UserBasketService>();

builder.Services.AddScoped<ImageService>();

builder.Services.AddScoped<CountriesService>();

builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<AddressLine>();

builder.Services.AddScoped<UserRatingService>();

builder.Services.AddAuthentication("ProiectAcademieCookies").AddCookie("ProiectAcademieCookies", options =>
{
    options.AccessDeniedPath = new PathString("/Account/Login");
    options.LoginPath = new PathString("/Account/Login");
});


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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


