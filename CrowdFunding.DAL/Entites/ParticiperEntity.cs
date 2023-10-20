using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Entites
{
    public class ParticiperEntity
    {
        public int Utilisateur_Id { get; set; }
        public int Contrepartie_Id { get; set; }
        public DateTime? Date { get; set; }
    }
}