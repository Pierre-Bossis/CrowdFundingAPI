using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos
{
    public class ProjetUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Nom { get; set; }
    }
}
