using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos.Projet
{
    public class ProjetCreateDto
    {
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Nom { get; set; }
        [Required]
        [Range(0.0, 100000)]
        public decimal Montant { get; set; }
        public int Utilisateur_Id { get; set; }
    }
}
