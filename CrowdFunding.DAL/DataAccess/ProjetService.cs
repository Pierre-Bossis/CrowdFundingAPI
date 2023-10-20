using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Repositories;
using CrowdFunding.Exceptions;
using Dapper;
using System.Data.SqlClient;

namespace CrowdFunding.DAL.DataAccess
{
    public class ProjetService : IProjetRepository
    {
        private readonly SqlConnection _connection;

        public ProjetService(SqlConnection connection)
        {
            _connection = connection;
        }
        public ProjetEntity Create(ProjetEntity projet)
        {
            if (projet is not null)
            {
                _connection.Open();
                string sql = "INSERT INTO Projet(Nom,Montant,Utilisateur_Id) VALUES (@nom,@montant,@utilisateur_id) SELECT SCOPE_IDENTITY();";
                var parameters = new
                {
                    nom = projet.Nom,
                    montant = projet.Montant,
                    utilisateur_id = projet.Utilisateur_Id
                };

                int id = _connection.ExecuteScalar<int>(sql, parameters);
                _connection.Close();
                ProjetEntity p = GetById(id);
                return p;
            }
            _connection.Close();
            return null;
        }

        public IEnumerable<ProjetEntity> GetAll()
        {
            _connection.Open();
            string sql = "SELECT * FROM Projet";
            IEnumerable<ProjetEntity>? projet = _connection.Query<ProjetEntity>(sql);
            _connection.Close();
            if(projet is not null)
                return projet;
            return null;
        }

        public ProjetEntity GetById(int id)
        {
            if (id != 0)
            {
                _connection.Open();
                string sql = "SELECT * FROM Projet WHERE Id = @id";
                var parameters = new { id = id };
                ProjetEntity? projet = _connection.QuerySingleOrDefault<ProjetEntity>(sql, parameters);
                _connection.Close();
                return projet;
            }
            return null;
        }

        public bool Update(ProjetEntity projet)
        {
            _connection.Open();
            string sql = "UPDATE Projet SET Nom = @nom, Montant = @montant WHERE Id = @id";
            var parameters = new { nom = projet.Nom, montant = projet.Montant, id = projet.Id };
            int rowsAffected = _connection.Execute(sql, parameters);
            _connection.Close();
            return rowsAffected > 0;
        }

        public ProjetEntity Upload(int id)
        {
            _connection.Open();

            string sql = "SELECT COUNT(*) FROM Contrepartie WHERE Projet_Id = @id";
            var parameters = new { id = id };
            int count = _connection.ExecuteScalar<int>(sql, parameters);

            if(count > 2)
            {
                string sql2 = "UPDATE Projet set DateMiseEnLigne = @datemiseenligne, DateFin = @datefin WHERE Id = @id";
                var parameters2 = new { datemiseenligne = DateTime.Now, datefin = DateTime.Now.AddMonths(6), id = id };
                _connection.Execute(sql2, parameters2);
                _connection.Close();

                //récupérer l'objet complet
                string sql3 = "SELECT * FROM Projet WHERE Id = @id";
                var parameters3 = new { id = id };
                ProjetEntity? projet = _connection.QuerySingle<ProjetEntity>(sql3, parameters3);
                if (projet is not null)
                    return projet;
                return null;
            }
            _connection.Close();
            throw new MinimalContrepartieException();

        }
    }
}
