using CrowdFunding.DAL.Entites;
using CrowdFunding.Dtos.Donner;

namespace CrowdFunding.Dtos.Mappers
{
    public static class DonnerMapper
    {
        public static DonnerEntity ToEntity(this DonnerDto don)
        {
            if (don is not null)
            {
                DonnerEntity d = new DonnerEntity()
                {
                    Projet_Id = don.Projet_Id,
                    Utilisateur_Id = don.Utilisateur_Id,
                    Montant = don.Montant,
                    Date = don.Date
                };
                return d;
            }
            return null;
        }

        public static DonnerDto ToDto(this DonnerEntity don)
        {
            if (don is not null)
            {
                DonnerDto d = new DonnerDto()
                {
                    Projet_Id = don.Projet_Id,
                    Utilisateur_Id = don.Utilisateur_Id,
                    Montant = don.Montant,
                    Date = don.Date
                };
                return d;
            }
            return null;
        }
    }
}
