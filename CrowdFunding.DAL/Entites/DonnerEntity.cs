using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Entites
{
    public class DonnerEntity
    {
        public int Utilisateur_Id { get; set; }
        public int Projet_Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal Montant { get; set; }
    }
}