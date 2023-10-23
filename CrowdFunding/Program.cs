using CrowdFunding.DAL.DataAccess;
using CrowdFunding.DAL.Interfaces;
using CrowdFunding.DAL.Repositories;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurService>();
builder.Services.AddScoped<IProjetRepository, ProjetService>();
builder.Services.AddScoped<IContrepartieRepository,ContrepartieService>();
builder.Services.AddScoped<IDonnerRepository,DonnerService>();
builder.Services.AddScoped<IParticiperRepository,ParticiperService>();
builder.Services.AddTransient(sp => new SqlConnection(configuration.GetConnectionString("techno")));


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

app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();
