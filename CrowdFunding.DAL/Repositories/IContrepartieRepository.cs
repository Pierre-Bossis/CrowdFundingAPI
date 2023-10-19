using CrowdFunding.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Repositories
{
    public interface IContrepartieRepository
    {
        ContrepartieEntity Create(ContrepartieEntity contrepartie);
        ContrepartieEntity GetById(int id);
        IEnumerable<ContrepartieEntity> GetAll();
        bool Update(ContrepartieEntity contrepartie);
    }
}
