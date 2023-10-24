using CrowdFunding.DAL.DataAccess;
using CrowdFunding.DAL.Interfaces;
using CrowdFunding.DAL.Repositories;
using CrowdFunding.Services;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//Service lié aux injections de dépendances
DependencyInjectionService.ConfigureDependencyInjection(builder.Services, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Temps d'expiration de la session
    options.Cookie.IsEssential = true;
});
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();
