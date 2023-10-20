using CrowdFunding.DAL.Entites;

namespace CrowdFunding.DAL.Repositories
{
    public interface IProjetRepository
    {
        ProjetEntity Create(ProjetEntity projet);
        ProjetEntity GetById(int id);
        IEnumerable<ProjetEntity> GetAll();
        bool Update(ProjetEntity projet);

        ProjetEntity Upload(int id);
    }
}
