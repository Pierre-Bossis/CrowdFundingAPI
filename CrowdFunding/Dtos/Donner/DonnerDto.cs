namespace CrowdFunding.Dtos.Donner
{
    public class DonnerDto
    {
        public int Utilisateur_Id { get; set; }
        public int Projet_Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Montant { get; set; }
    }
}
