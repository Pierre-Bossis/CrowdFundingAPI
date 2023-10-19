CREATE TABLE [dbo].[Projet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Nom] VARCHAR(50) NOT NULL,
	[Montant] DECIMAL NOT NULL,
	[DateCreation] DATETIME NOT NULL,
	[DateMiseEnLigne] DATETIME,
	[DateFin] DATETIME,
	[Utilisateur_Id] INT NOT NULL,
	FOREIGN KEY (Utilisateur_Id) REFERENCES Utilisateur(Id)
)
