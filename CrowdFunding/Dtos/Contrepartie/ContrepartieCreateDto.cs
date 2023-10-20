using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos.Contrepartie
{
    public class ContrepartieCreateDto
    {
        public decimal Montant { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public int Projet_Id { get; set; }
    }
}
