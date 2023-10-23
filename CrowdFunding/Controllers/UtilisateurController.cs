using CrowdFunding.DAL.DataAccess;
using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Interfaces;
using CrowdFunding.DAL.Repositories;
using CrowdFunding.Dtos;
using CrowdFunding.Dtos.Donner;
using CrowdFunding.Dtos.Mappers;
using CrowdFunding.Dtos.Utilisateur;
using CrowdFunding.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurRepository _repo;

        public UtilisateurController(IUtilisateurRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterDto utilisateur)
        {
            try
            {
                UtilisateurLoggedDto? u = _repo.Register(utilisateur.ToEntityRegister()).ToLoggedDto();
                //return Created();
                HttpContext.Session.SetInt32("Id", u.Id);
                HttpContext.Session.SetString("Nom", u.Nom);
                HttpContext.Session.SetString("Prenom", u.Prenom);
                HttpContext.Session.SetString("Email", u.Email);
                return Ok(u);
            }
            catch (EmailDuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]UtilisateurLoginDto utilisateur)
        {
            try
            {
                UtilisateurLoggedDto u = _repo.Login(utilisateur.ToEntity()).ToLoggedDto();
                if(u is not null)
                {
                    HttpContext.Session.SetInt32("Id", u.Id);
                    HttpContext.Session.SetString("Nom", u.Nom);
                    HttpContext.Session.SetString("Prenom", u.Prenom);
                    HttpContext.Session.SetString("Email", u.Email);
                    return Ok(u);
                }
                return BadRequest("Erreur lors de la connexion.");
            }
            catch (WrongsCredentialsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            //renvoie un des dto complet sans le mot de passe
            IEnumerable<UtilisateurLoggedDto>? utilisateurs =  _repo.GetAll().Select(u=>u.ToLoggedDto());

            if(utilisateurs is not null)
                return Ok(utilisateurs);
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            UtilisateurDto? utilisateur = _repo.GetById(id).ToDto();
            if(utilisateur is not null)
                return Ok(utilisateur);
            return NotFound();
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            _repo.Delete((int)HttpContext.Session.GetInt32("Id"));
            HttpContext.Session.Clear();
            return Ok("Compte supprimé.");
        }
    }
}
