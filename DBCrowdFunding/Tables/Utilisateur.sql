﻿CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Nom] VARCHAR(50) NOT NULL,
	[Prenom] VARCHAR(50) NOT NULL,
	[Email] VARCHAR(50) NOT NULL UNIQUE,
	[MotDePasse] VARCHAR(100) NOT NULL
)
