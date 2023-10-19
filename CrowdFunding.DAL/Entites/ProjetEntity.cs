using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Entites
{
    public class ProjetEntity
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Montant { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateMiseEnLigne { get; set; }
        public DateTime? DateFin { get; set; }
        public int Utilisateur_Id { get; set; }
    }
}