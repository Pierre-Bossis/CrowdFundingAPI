using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Repositories;
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
                string sql = "INSERT INTO Projet VALUES (@nom,@montant,@datecreation,@datemiseenligne,@datefin,@utilisateur_id) SELECT SCOPE_IDENTITY();";
                var parameters = new
                {
                    nom = projet.Nom,
                    montant = projet.Montant,
                    datecreation = projet.DateCreation,
                    datemiseenligne = (object?)projet.DateMiseEnLigne ?? DBNull.Value,
                    datefin = (object?)projet.DateFin ?? DBNull.Value,
                    utilisateur_id = projet.Utilisateur_Id
                };

                projet.Id = _connection.ExecuteScalar<int>(sql, parameters);
                _connection.Close();
                return projet;
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
            string sql = "UPDATE Projet SET Nom = @nom WHERE Id = @id";
            var parameters = new { nom = projet.Nom, id = projet.Id };
            int rowsAffected = _connection.Execute(sql, parameters);
            _connection.Close();
            return rowsAffected > 0;
        }
    }
}
