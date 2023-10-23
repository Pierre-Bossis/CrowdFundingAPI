using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Interfaces;
using CrowdFunding.Exceptions;
using Dapper;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CrowdFunding.DAL.DataAccess
{
    public class UtilisateurService : IUtilisateurRepository
    {
        private readonly SqlConnection _connection;

        public UtilisateurService(SqlConnection connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// Enregistrer un user
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public UtilisateurEntity Register(UtilisateurEntity utilisateur)
        {
            if (IfEmailExist(utilisateur.Email)) throw new EmailDuplicateException();

            _connection.Open();

            utilisateur.MotDePasse = HashMotDePasse(utilisateur.MotDePasse);

            string sql = "INSERT INTO [Utilisateur] VALUES (@nom,@prenom,@email,@motdepasse) SELECT SCOPE_IDENTITY();";
            var parameters = new { nom = utilisateur.Nom, prenom = utilisateur.Prenom, email = utilisateur.Email, motdepasse = utilisateur.MotDePasse };
            utilisateur.Id = _connection.ExecuteScalar<int>(sql, parameters);
            _connection.Close();
            return utilisateur;
        }

        /// <summary>
        /// Obtenir liste des users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UtilisateurEntity> GetAll()
        {
            _connection.Open();
            string sql = "select * from Utilisateur";
            IEnumerable<UtilisateurEntity> utilisateurs = _connection.Query<UtilisateurEntity>(sql);
            _connection.Close();
            if (utilisateurs is not null)
            {
                _connection.Close();
                return utilisateurs;
            }
            _connection.Close();
            return null;
        }

        /// <summary>
        /// Obtenir un user par son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UtilisateurEntity GetById(int id)
        {
            _connection.Open();
            string sql = "select * from Utilisateur where Id = @monId";
            var parameters = new { monId = id };
            UtilisateurEntity? utilisateur = _connection.QuerySingleOrDefault<UtilisateurEntity>(sql, parameters);
            if (utilisateur is not null)
            {
                _connection.Close();
                return utilisateur;
            }
            _connection.Close();
            return null;
        }

        /// <summary>
        /// Se connecter au serveur
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        /// <exception cref="WrongsCredentialsException"></exception>
        public UtilisateurEntity Login(UtilisateurEntity utilisateur)
        {
            _connection.Open();

            string PasswordHash = HashMotDePasse(utilisateur.MotDePasse);
            string sql = "SELECT * from Utilisateur WHERE Email = @email";
            var parameters = new { email = utilisateur.Email };
            UtilisateurEntity? u = _connection.QuerySingleOrDefault<UtilisateurEntity>(sql, parameters);
            _connection.Close();

            if (u != null && PasswordHash == u.MotDePasse)
                return u;
            throw new WrongsCredentialsException();
        }

        /// <summary>
        /// Hasher un mot de passe pour le register ou le login
        /// </summary>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        private string HashMotDePasse(string motDePasse)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));
                return Convert.ToBase64String(hashBytes);
            }
        }


        //methode si adresse email est déjà utilisée
        private bool IfEmailExist(string email)
        {
            _connection.Open();

            string sql = "SELECT COUNT(*) FROM Utilisateur WHERE Email = @email";
            var parameters = new { email = email };
            //if res plus grand que 0 return false
            int count = _connection.Execute(sql, parameters);
            _connection.Close();
            if (count > 0) return true;
            return false;
        }


    }
}
