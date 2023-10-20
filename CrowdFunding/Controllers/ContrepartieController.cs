using CrowdFunding.DAL.Repositories;
using CrowdFunding.Dtos;
using CrowdFunding.Dtos.Mappers;
using CrowdFunding.Dtos.Participer;
using Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContrepartieController : ControllerBase
    {
        private readonly IContrepartieRepository _repo;
        private readonly IParticiperRepository _repoParticiper;

        public ContrepartieController(IContrepartieRepository repo, IParticiperRepository repoParticiper)
        {
            _repo = repo;
            _repoParticiper = repoParticiper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ContrepartieDto> contreparties = _repo.GetAll().Select(c => c.ToDto());
            if (contreparties is not null)
                return Ok(contreparties);
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id != 0)
            {
                ContrepartieDto? contrepartie = _repo.GetById(id).ToDto();
                if (contrepartie is not null)
                    return Ok(contrepartie);
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(ContrepartieDto contrepartie)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            try
            {
                if (contrepartie is not null)
                {
                    ContrepartieDto? c = _repo.Create(contrepartie.ToEntity()).ToDto();
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
        public IActionResult Update(int id,ContrepartieDto contrepartie)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            try
            {
                if (contrepartie is not null)
                {
                    bool success = _repo.Update(contrepartie.ToEntity());
                    if (success)
                        return Ok("Mise à jour réussie.");
                    return BadRequest();
                }
                return BadRequest();
            }
            catch (MontantDupliqueException e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("{id:int}/participer")]
        public IActionResult Participation(int id, ParticiperDto participation)
        {
            if (HttpContext.Session.GetInt32("Id") is null) return BadRequest("Vous devez être connecté.");

            try
            {
                participation.Utilisateur_Id = (int)HttpContext.Session.GetInt32("Id");
                participation.Contrepartie_Id = id;

                _repoParticiper.Participer(participation.ToEntity());
                return Ok("Participation enregistrée.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la participation : " + ex.Message);
            }
        }
    }
}
