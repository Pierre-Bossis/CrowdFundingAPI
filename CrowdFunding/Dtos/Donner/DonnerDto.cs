namespace CrowdFunding.Dtos.Donner
{
    public class DonnerDto
    {
        public int Utilisateur_Id { get; set; }
        public int Projet_Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal Montant { get; set; }
    }
}
