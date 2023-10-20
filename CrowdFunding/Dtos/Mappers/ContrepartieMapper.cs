using CrowdFunding.DAL.Entites;
using CrowdFunding.Dtos.Contrepartie;

namespace CrowdFunding.Dtos.Mappers
{
    public static class ContrepartieMapper
    {
        #region Full Dto
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

        #endregion

        #region Create Dto
        
        public static ContrepartieEntity ToEntityCreate(this ContrepartieCreateDto contrepartie)
        {
            if(contrepartie is not null)
            {
                ContrepartieEntity c = new ContrepartieEntity()
                {
                    Montant = contrepartie.Montant,
                    Description = contrepartie.Description,
                    Projet_Id = contrepartie.Projet_Id
                };
                return c;
            }
            return null;
        }

        #endregion
    }
}
