using CrowdFunding.DAL.Entites;
using CrowdFunding.Dtos.Participer;

namespace CrowdFunding.Dtos.Mappers
{
    public static class ParticiperMapper
    {
        public static ParticiperEntity ToEntity(this ParticiperDto participation)
        {
            if(participation is not null)
            {
                ParticiperEntity p = new ParticiperEntity()
                {
                    Utilisateur_Id = participation.Utilisateur_Id,
                    Contrepartie_Id = participation.Contrepartie_Id,
                    Date = participation.Date
                };
                return p;
            }
            return null;
        }

        public static ParticiperDto ToDto(this ParticiperEntity participation)
        {
            if(participation is not null)
            {
                ParticiperDto p = new ParticiperDto()
                {
                    Utilisateur_Id = participation.Utilisateur_Id,
                    Contrepartie_Id = participation.Contrepartie_Id,
                    Date = participation.Date
                };
                return p;
            }
            return null;
        }
    }
}
