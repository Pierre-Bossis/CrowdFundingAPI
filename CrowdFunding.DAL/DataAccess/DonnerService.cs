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
    public class DonnerService : IDonnerRepository
    {
        private readonly SqlConnection _connection;

        public DonnerService(SqlConnection connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// Action pour faire un don
        /// </summary>
        /// <param name="don"></param>
        public void Don(DonnerEntity don)
        {
            _connection.Open();

            string sql = "INSERT INTO Donner VALUES (@utilisateur_id,@projet_id,@date,@montant)";
            var parameters = new { utilisateur_id = don.Utilisateur_Id, projet_id = don.Projet_Id, date = don.Date, montant = don.Montant };
            _connection.Execute(sql, parameters);

            _connection.Close();
        }
        public IEnumerable<DonnerEntity> GetAll()
        {
            _connection.Open();

            string sql = "SELECT * FROM Donner";
            IEnumerable<DonnerEntity>? donations = _connection.Query<DonnerEntity>(sql);
            _connection.Close();
            if (donations is not null)
                return donations;
            return null;
        }
    }
}
