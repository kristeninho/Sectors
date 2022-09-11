using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories;

namespace SectorsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly SectorsRepository _sectorRepository;
        public SectorsController(SectorsRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        // GET: api/GetAllSectorsSeperatedByCategories
        [HttpGet("GetAllSectorsSeperatedByCategories")]
        public async Task<ActionResult<List<SectorDTO>>> GetAllSectorsSeperatedByCategories()
        {
            var sectors = await _sectorRepository.GetSectorsFilteredByCategoryAsync();

            if (sectors == null)
            {
                return NotFound("Sectors not found");
            }

            return sectors;
        }
    }
}
