using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DAL.DataAccess
{
    public class ParticiperService : IParticiperRepository
    {
        private readonly SqlConnection _connection;

        public ParticiperService(SqlConnection connection)
        {
            _connection = connection;    
        }

        public IEnumerable<ParticiperEntity> GetAll()
        {
            _connection.Open();

            string sql = "SELECT * FROM Participer";
            IEnumerable<ParticiperEntity>? participations = _connection.Query<ParticiperEntity>(sql);
            _connection.Close();
            if (participations is not null)
                return participations;
            return null;

        }

        public void Participer(ParticiperEntity participation)
        {
            _connection.Open();

            string sql = "INSERT INTO Participer VALUES (@utilisateur_id,@contrepartie_id,@date)";
            var parameters = new { utilisateur_id = participation.Utilisateur_Id, contrepartie_id = participation.Contrepartie_Id, date = participation.Date };
            _connection.Execute(sql, parameters);

            _connection.Close();
        }
    }
}
