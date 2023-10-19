using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos
{
    public class ProjetDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Nom { get; set; }
        [Required]
        [Range(0.0,100000)]
        public decimal Montant { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DateMiseEnLigne { get; set; }
        public DateTime? DateFin { get; set; }
        public int Utilisateur_Id { get; set; }
    }
}
