CREATE TABLE [dbo].[Donner]
(
    [Utilisateur_Id] INT NOT NULL,
    [Projet_Id] INT NOT NULL,
    [Date] DATETIME DEFAULT GETDATE(),
    [Montant] DECIMAL(10, 2) NOT NULL,
    --PRIMARY KEY (Utilisateur_Id, Projet_Id),
    FOREIGN KEY (Utilisateur_Id) REFERENCES Utilisateur(Id),
    FOREIGN KEY (Projet_Id) REFERENCES Projet(Id)
)
