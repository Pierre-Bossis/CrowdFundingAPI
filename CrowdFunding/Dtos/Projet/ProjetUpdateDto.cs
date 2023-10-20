using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos
{
    public class ProjetUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Nom { get; set; }
        [Required]
        [Range(0.0, 100000)]
        public decimal Montant { get; set; }

    }
}
