using CrowdFunding.DAL.Entites;
using CrowdFunding.DAL.Repositories;
using CrowdFunding.Dtos;
using CrowdFunding.Dtos.Contrepartie;
using CrowdFunding.Dtos.Donner;
using CrowdFunding.Dtos.Mappers;
using CrowdFunding.Dtos.Projet;
using CrowdFunding.Exceptions;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetController : ControllerBase
    {//create et upload, nameprojetexception
        private readonly IProjetRepository _repo;
        private readonly IDonnerRepository _repoDonation;
        private readonly IContrepartieRepository _repoContrepartie;

        public ProjetController(IProjetRepository repo, IDonnerRepository repoDonation, IContrepartieRepository repoContrepartie)
        {
            _repo = repo;
            _repoDonation = repoDonation;
            _repoContrepartie = repoContrepartie;
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
        public IActionResult Create(ProjetCreateDto projet)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");
            try
            {
                projet.Utilisateur_Id = (int)HttpContext.Session.GetInt32("Id");
                ProjetDto? p = _repo.Create(projet.ToEntityCreate()).ToDto();
                if (p is not null)
                    //return Created();
                    return Ok(p);
                return BadRequest("Erreur lors de la création du projet.");
            }
            catch (ProjetNameDuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //créer une contrepartie à un projet, cohérent qu'il soit ici
        [HttpPost]
        [Route("{id:int}/createcontrepartie")]
        public IActionResult CreateContrepartie(int id,ContrepartieCreateDto contrepartie)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            //creation contrepartie uniquement possible par le même  user que le createur du projet
            if (_repo.GetById(id).Utilisateur_Id != HttpContext.Session.GetInt32("Id"))
                return BadRequest("La contrepartie ne peut être faite que par le créateur du projet.");

            try
            {
                if (contrepartie is not null)
                {
                    contrepartie.Projet_Id = id;
                    ContrepartieDto? c = _repoContrepartie.Create(contrepartie.ToEntityCreate()).ToDto();
                    if (c is not null)
                        //return created();
                        return Ok(c);
                    return BadRequest();
                }
                return BadRequest();
            }
            catch (MontantDupliqueException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public IActionResult Update(int id, ProjetUpdateDto projet)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            try
            {
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
            catch (ProjetNameDuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
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
