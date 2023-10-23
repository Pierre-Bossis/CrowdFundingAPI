using CrowdFunding.DAL.DataAccess;
using CrowdFunding.DAL.Interfaces;
using CrowdFunding.DAL.Repositories;
using System.Data.SqlClient;

namespace CrowdFunding.Services
{
    public static class DependencyInjectionService
    {
        public static void ConfigureDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(sp => new SqlConnection(configuration.GetConnectionString("techno")));
            services.AddScoped<IUtilisateurRepository, UtilisateurService>();
            services.AddScoped<IProjetRepository, ProjetService>();
            services.AddScoped<IContrepartieRepository, ContrepartieService>();
            services.AddScoped<IDonnerRepository, DonnerService>();
            services.AddScoped<IParticiperRepository, ParticiperService>();
        }

    }
}
