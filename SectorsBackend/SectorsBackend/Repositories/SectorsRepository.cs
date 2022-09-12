using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.DTOs;
using SectorsBackend.Models;

namespace SectorsBackend.Repositories
{
	public class SectorsRepository
	{
		private readonly IDbContextFactory<AppDbContext> _context;

		public SectorsRepository(IDbContextFactory<AppDbContext> context)
		{
			_context = context;
		}

		public virtual async Task<ActionResult<List<SectorDTO>>> GetSectorsFilteredByCategoryAsync()
		{
			using var context = _context.CreateDbContext();

			var allSectors = await context.Sectors.ToListAsync();

			var levelZeroSectors = allSectors.Where(a => a.ParentId == null).ToList();

			var sectorsList = (levelZeroSectors.Select(sector => ConfigureSectorDTORecursively(sector, allSectors, 0))).ToList();

			return sectorsList;
		}

		private SectorDTO ConfigureSectorDTORecursively(Sector sector, List<Sector> sectors, int level)
		{
			if (sector.HasSubSectors == false)
			{
				return new SectorDTO()
				{
					Sector = sector,
					Sectors = null
				};
			}

			var subSectors = new List<SectorDTO>();

			foreach(var s in sectors.Where(x => x.ParentId == sector.Id))
			{
				subSectors.Add(ConfigureSectorDTORecursively(s, sectors, level + 1));
			}

			return new SectorDTO()
			{
				Sector = sector,
				Sectors = subSectors
			};
		}
	}
}
