using Microsoft.EntityFrameworkCore;
using Performance_test_csharp.Data;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Services;
using Performance_test_csharp.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Config email service (añadir dependencias)
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeportiveSpace, DeportiveSpaceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
//config de la base de datso
builder.Services.AddDbContext<AppDbcontext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//todo: esto es para que postgres no cansonee con el utc de las fechas (:)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
