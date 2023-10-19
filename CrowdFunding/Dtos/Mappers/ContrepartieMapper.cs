using CrowdFunding.DAL.Entites;

namespace CrowdFunding.Dtos.Mappers
{
    public static class ContrepartieMapper
    {
        public static ContrepartieEntity ToEntity(this ContrepartieDto contrepartie)
        {
            if (contrepartie is not null)
            {
                ContrepartieEntity c = new ContrepartieEntity()
                {
                    Id = contrepartie.Id,
                    Montant = contrepartie.Montant,
                    Description = contrepartie.Description,
                    Projet_Id = contrepartie.Projet_Id
                };
                return c;
            }
            return null;
        }

        public static ContrepartieDto ToDto(this ContrepartieEntity contrepartie)
        {
            if (contrepartie is not null)
            {
                ContrepartieDto c = new ContrepartieDto()
                {
                    Id = contrepartie.Id,
                    Montant = contrepartie.Montant,
                    Description = contrepartie.Description,
                    Projet_Id = contrepartie.Projet_Id
                };
                return c;
            }
            return null;
        }
    }
}
