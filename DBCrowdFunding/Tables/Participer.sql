CREATE TABLE [dbo].[Participer]
(
	[Utilisateur_Id] INT NOT NULL,
	[Contrepartie_Id] INT NOT NULL,
	[Date] DATETIME DEFAULT GETDATE(),
	PRIMARY KEY (Utilisateur_Id,Contrepartie_Id),
	FOREIGN KEY (Utilisateur_Id) REFERENCES Utilisateur(Id),
	FOREIGN KEY (Contrepartie_Id) REFERENCES Contrepartie(Id)
)
