CREATE TABLE [dbo].[Contrepartie]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Montant] DECIMAL(10, 2) NOT NULL,
	[Description] VARCHAR(300) NOT NULL,
	[Projet_Id] INT NOT NULL,
	FOREIGN KEY (Projet_Id) REFERENCES Projet(Id),
	CONSTRAINT UQ_UniqueMontantParProjet UNIQUE (Projet_Id, Montant)
)
