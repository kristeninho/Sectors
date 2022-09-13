using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;
using SectorsBackend.Repositories.Interfaces;

namespace SectorsBackend.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly ISectorsRepository _sectorRepository;
        public SectorsController(ISectorsRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        // GET: api/GetAllSectorsSeparatedByCategories
        [HttpGet("GetAllSectorsSeparatedByCategories")]
        public async Task<ActionResult<List<SectorDTO>>> GetAllSectorsSeparatedByCategories()
        {
            var sectors = await _sectorRepository.GetSectorsSeparatedByCategoryAsync();

            if (sectors == null || sectors.Value.Count() == 0)
            {
                return NotFound("Sectors not found");
            }

            return sectors;
        }
    }
}
