using CrowdFunding.DAL.Entites;

namespace CrowdFunding.Dtos.Mappers
{
    public static class UtilisateurMapper
    {
        /// <summary>
        /// Convertir un Dto utilisateur complet en entité
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public static UtilisateurEntity ToEntity(this UtilisateurDto utilisateur)
        {
            if(utilisateur is not null)
            {
                UtilisateurEntity u = new UtilisateurEntity()
                {
                    Id = utilisateur.Id,
                    Nom = utilisateur.Nom,
                    Prenom = utilisateur.Prenom,
                    Email = utilisateur.Email,
                    MotDePasse = utilisateur.MotDePasse
                };
                return u;
            }
            return null;
        }

        /// <summary>
        /// Convertir une entité utilisateur complète en Dto
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public static UtilisateurDto ToDto(this UtilisateurEntity utilisateur)
        {
            if(utilisateur is not null)
            {
                UtilisateurDto u = new UtilisateurDto()
                {
                    Id = utilisateur.Id,
                    Nom = utilisateur.Nom,
                    Prenom = utilisateur.Prenom,
                    Email = utilisateur.Email,
                    MotDePasse = utilisateur.MotDePasse
                };
                return u;
            }
            return null;
        }

        /// <summary>
        /// Methode pour se loger avec le utilisateurlogindto
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public static UtilisateurEntity ToEntity(this UtilisateurLoginDto utilisateur)
        {
            if(utilisateur is not null)
            {
                UtilisateurEntity u = new UtilisateurEntity()
                {
                    Email = utilisateur.Email,
                    MotDePasse = utilisateur.MotDePasse
                };
                return u;
            }
            return null;
        }

        /// <summary>
        /// Methode pour recevoir ses infos en tant qu'user loggé, sans mot de passe donc
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public static UtilisateurLoggedDto ToLoggedDto(this UtilisateurEntity utilisateur)
        {
            if(utilisateur is not null)
            {
                UtilisateurLoggedDto u = new UtilisateurLoggedDto()
                {
                    Id= utilisateur.Id,
                    Email = utilisateur.Email,
                    Nom= utilisateur.Nom,
                    Prenom = utilisateur.Prenom,
                };
                return u;
            }
            return null;
        }
    }
}
