using CrowdFunding.DAL.Repositories;
using CrowdFunding.Dtos.Donner;
using CrowdFunding.Dtos.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonnerController : ControllerBase
    {
        private readonly IDonnerRepository _repo;

        public DonnerController(IDonnerRepository repo)
        {
            _repo = repo;           
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<DonnerDto> Donations = _repo.GetAll().Select(d=>d.ToDto());
            if(Donations is not null)
                return Ok(Donations);
            return NotFound();
        }
    }
}
