using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos
{
    public class UtilisateurLoggedDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Nom { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
