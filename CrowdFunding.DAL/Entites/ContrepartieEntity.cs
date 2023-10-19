using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.Entites
{
    public class ContrepartieEntity
    {
        public int Id { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
        public int Projet_Id { get; set; }
    }
}
