using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaP2.DataAccess.Repository;
using PruebaTecnicaP2.DataAccess;
using PruebaTecnicaP2.Models.Helpers;
using PruebaTecnicaP2.Helpers.LoggerManager;
using PruebaTecnicaP2.Services;
using PruebaTecnicaP2.Helpers.AutoMapping;
using PruebaTecnicaP2.Providers.Puntosleal;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<DbContext, ProyectoContext>();
builder.Services.AddTransient(typeof(IDataRepository<>), typeof(DataRepository<>));
builder.Services.AddTransient<ILog, Log>();
builder.Services.AddTransient<IPuntosService, PuntosService>();
builder.Services.AddTransient<IDataPuntosleal, DataPuntosleal>();

builder.Services.AddAutoMapper(c => c.AddProfile<AutoMappingHelper>(), typeof(Program).Assembly);
builder.Services.Configure<PuntoslealSettings>(Configuration.GetSection("PuntoslealSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
