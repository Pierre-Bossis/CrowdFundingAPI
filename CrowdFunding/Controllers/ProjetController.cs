using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Repositories;
using CrowdFunding.Dtos;
using CrowdFunding.Dtos.Donner;
using CrowdFunding.Dtos.Mappers;
using CrowdFunding.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetController : ControllerBase
    {
        private readonly IProjetRepository _repo;
        private readonly IDonnerRepository _repoDonation;

        public ProjetController(IProjetRepository repo, IDonnerRepository repoDonation)
        {
            _repo = repo;
            _repoDonation = repoDonation;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ProjetDto> projets = _repo.GetAll().Select(p => p.ToDto());
            if (projets is not null)
                return Ok(projets);
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            ProjetDto? projet = _repo.GetById(id).ToDto();
            if (projet is not null)
                return Ok(projet);
            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(ProjetDto projet)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            projet.Utilisateur_Id = (int)HttpContext.Session.GetInt32("Id");
            ProjetDto? p = _repo.Create(projet.ToEntity()).ToDto();
            if (p is not null)
                //return Created();
                return Ok(p);
            return BadRequest("Erreur lors de la création du projet.");
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public IActionResult Update(int id, ProjetUpdateDto projet)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            //vérifier si le projet n'est pas déjà en ligne avant d'update
            ProjetDto? p = _repo.GetById(id).ToDto();
            if (p is not null && p.DateMiseEnLigne is not null)
                return BadRequest("Un projet mis en ligne ne peut être modifié.");

            //Vérifie si c'est bien le créateur du projet qui essaye de le modifier.
            if (p.Utilisateur_Id != HttpContext.Session.GetInt32("Id"))
                return BadRequest("Seul le créateur de ce projet peut le modifier.");

            //vers l'update
            projet.Id = id;
            bool Success = _repo.Update(projet.ToEntityUpdate());
            if (Success)
                return Ok("Mise à jour réussie.");
            return BadRequest();
        }

        [HttpPost]
        [Route("{id:int}/donation")]
        public IActionResult FaireDon(int id, [FromBody] DonnerDto don)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");
            try
            {
                don.Projet_Id = id;
                don.Utilisateur_Id = (int)HttpContext.Session.GetInt32("Id");
                _repoDonation.Don(don.ToEntity());
                return Ok("Don effectué avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors du don : " + ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}/upload")]
        public IActionResult Upload(int id)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");
            //upload possible que si c'est le createur du projet qui upload

            //Vérifie si c'est bien le créateur du projet qui tente de le mettre en ligne.
            if (_repo.GetById(id).Utilisateur_Id != HttpContext.Session.GetInt32("Id"))
                return BadRequest("Seul le créateur de ce projet peut le mettre en ligne.");

            try
            {
                ProjetDto? projet = _repo.Upload(id).ToDto();
                if (projet is not null)
                    return Ok(projet);
                return NotFound();

            }
            catch (MinimalContrepartieException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
