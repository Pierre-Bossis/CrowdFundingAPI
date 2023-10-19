using CrowdFunding.DAL.Repositories;
using CrowdFunding.Dtos.Mappers;
using CrowdFunding.Dtos.Participer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticiperController : ControllerBase
    {
        private readonly IParticiperRepository _repo;

        public ParticiperController(IParticiperRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ParticiperDto>? participations = _repo.GetAll().Select(p=>p.ToDto());
            if(participations is not null)
                return Ok(participations);
            return NotFound();
        }
    }
}
