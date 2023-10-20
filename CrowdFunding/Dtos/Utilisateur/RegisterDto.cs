using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Dtos.Utilisateur
{
    public class RegisterDto
    {
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
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$%^&+=!])(?!.*\\s).{8,}$", ErrorMessage = "Votre mot de passe doit contenir une majuscule, des chiffres et 8 caractères")]
        public string MotDePasse { get; set; }
    }
}
