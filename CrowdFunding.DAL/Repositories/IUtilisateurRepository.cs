using CrowdFunding.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Interfaces
{
    public interface IUtilisateurRepository
    {
        UtilisateurEntity Register(UtilisateurEntity utilisateur);
        UtilisateurEntity Login(UtilisateurEntity utilisateur);
        IEnumerable<UtilisateurEntity> GetAll();
        UtilisateurEntity GetById(int id);
        void Delete(int id);
    }
}