using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Repositories;
using Dapper;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.DataAccess
{
    public class ContrepartieService : IContrepartieRepository
    {
        private readonly SqlConnection _connection;

        public ContrepartieService(SqlConnection connection)
        {
            _connection = connection;
        }

        public ContrepartieEntity Create(ContrepartieEntity contrepartie)
        {
            if (contrepartie is not null)
            {
                //vérifie si le montant existe déjà pour le même projet.
                string sqlVerif = "SELECT COUNT(*) FROM Contrepartie WHERE Montant = @montant AND Projet_Id = @projet_id";
                var parametersVerif = new { montant = contrepartie.Montant,projet_id = contrepartie.Projet_Id };
                int count = _connection.ExecuteScalar<int>(sqlVerif,parametersVerif);
                if (count > 0) throw new MontantDupliqueException();

                _connection.Open();
                string sql = "INSERT INTO Contrepartie VALUES (@montant,@description,@projet_id) SELECT SCOPE_IDENTITY();";
                var parameters = new
                {
                    montant = contrepartie.Montant,
                    description = contrepartie.Description,
                    projet_id = contrepartie.Projet_Id
                };

                contrepartie.Id = _connection.ExecuteScalar<int>(sql, parameters);
                _connection.Close();
                return contrepartie;
            }
            _connection.Close();
            return null;
        }

        public IEnumerable<ContrepartieEntity> GetAll()
        {
            _connection.Open();
            string sql = "SELECT * FROM Contrepartie";
            IEnumerable<ContrepartieEntity>? contreparties = _connection.Query<ContrepartieEntity>(sql);
            _connection.Close();
            if (contreparties is not null)
                return contreparties;
            return null;
        }

        public IEnumerable<ContrepartieEntity> GetAllForProjet(int id)
        {
            _connection.Open();

            string sql = "SELECT * FROM Contrepartie WHERE Projet_Id = @projet_id";
            var parameters = new { projet_id = id };
            IEnumerable<ContrepartieEntity>? contreparties = _connection.Query<ContrepartieEntity>(sql, parameters);
            _connection.Close();
            if(contreparties is not null)
                return contreparties;
            return null;
        }

        public ContrepartieEntity GetById(int id)
        {
            if (id != 0)
            {
                _connection.Open();
                string sql = "SELECT * FROM Contrepartie WHERE Id = @id";
                var parameter = new { id = id };
                ContrepartieEntity? contrepartie = _connection.QuerySingleOrDefault<ContrepartieEntity>(sql, parameter);
                _connection.Close();
                if(contrepartie is not null)
                    return contrepartie;
                return null;
            }
            return null;
        }

        public bool Update(ContrepartieEntity contrepartie)
        {
            _connection.Open();

            //vérifie si le montant existe déjà pour le même projet.
            string sqlVerif = "SELECT COUNT(*) FROM Contrepartie WHERE Montant = @montant AND Projet_Id = @projet_id";
            var parametersVerif = new { montant = contrepartie.Montant, projet_id = contrepartie.Projet_Id };
            int count = _connection.ExecuteScalar<int>(sqlVerif, parametersVerif);
            if (count > 0) throw new MontantDupliqueException();

            string sql = "UPDATE Contrepartie SET Description = @description, Montant = @montant WHERE Id = @id";
            var parameters = new { description = contrepartie.Description, montant = contrepartie.Montant, id = contrepartie.Id };
            int rowsAffected = _connection.Execute(sql, parameters);
            _connection.Close();
            return rowsAffected > 0;
        }
    }
}
