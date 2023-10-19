using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos
{
    public class ContrepartieDto
    {
        public int Id { get; set; }
        [Required]
        public decimal Montant { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public int Projet_Id { get; set; }
    }
}