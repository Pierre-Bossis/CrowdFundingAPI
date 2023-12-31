﻿using CrowdFunding.DAL.Entites;
using CrowdFunding.Dtos.Projet;

namespace CrowdFunding.Dtos.Mappers
{
    public static class ProjetMapper
    {
        #region Dto Full
        public static ProjetEntity ToEntity(this ProjetDto projet)
        {
            if (projet is not null)
            {
                ProjetEntity p = new ProjetEntity()
                {
                    Id = projet.Id,
                    Nom = projet.Nom,
                    Montant = projet.Montant,
                    DateCreation = projet?.DateCreation,
                    DateMiseEnLigne = projet?.DateMiseEnLigne,
                    DateFin = projet?.DateFin,
                    Utilisateur_Id = projet.Utilisateur_Id
                };
                return p;
            }
            return null;
        }

        public static ProjetDto ToDto(this ProjetEntity projet)
        {
            if (projet is not null)
            {
                ProjetDto p = new ProjetDto()
                {
                    Id = projet.Id,
                    Nom = projet.Nom,
                    Montant = projet.Montant,
                    DateCreation = projet?.DateCreation,
                    DateMiseEnLigne = projet?.DateMiseEnLigne,
                    DateFin = projet?.DateFin,
                    Utilisateur_Id = projet.Utilisateur_Id
                };
                return p;
            }
            return null;
        }
        #endregion

        #region Dto Update
        public static ProjetEntity ToEntityUpdate(this ProjetUpdateDto projet)
        {
            if (projet is not null)
            {
                ProjetEntity p = new ProjetEntity()
                {
                    Id = projet.Id,
                    Nom = projet.Nom,
                    Montant = projet.Montant
                };
                return p;
            }
            return null;
        }
        #endregion

        #region Dto Create

        public static ProjetEntity ToEntityCreate(this ProjetCreateDto projet)
        {
            if(projet is not null)
            {
                ProjetEntity p = new ProjetEntity()
                {
                    Nom = projet.Nom,
                    Montant = projet.Montant,
                    Utilisateur_Id = projet.Utilisateur_Id
                };
                return p;
            }
            return null;
        }

        #endregion
    }
}
