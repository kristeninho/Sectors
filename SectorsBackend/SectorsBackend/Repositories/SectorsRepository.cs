using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories.Interfaces;

namespace SectorsBackend.Repositories
{
	public class SectorsRepository: ISectorsRepository
	{
		private readonly IDbContextFactory<AppDbContext> _context;

		public SectorsRepository(IDbContextFactory<AppDbContext> context)
		{
			_context = context;
		}

		public virtual async Task<ActionResult<List<SectorDTO>>> GetSectorsSeparatedByCategoryAsync()
		{
			using var context = _context.CreateDbContext();

			var allSectors = await context.Sectors.ToListAsync();

			var levelZeroSectors = allSectors.Where(a => a.ParentId == null).ToList();

			var sectorsList = (levelZeroSectors.Select(sector => ConfigureSectorDTORecursive(sector, allSectors, 0))).ToList();

			return sectorsList;
		}

		private SectorDTO ConfigureSectorDTORecursive(Sector sector, List<Sector> sectors, int level)
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
				subSectors.Add(ConfigureSectorDTORecursive(s, sectors, level + 1));
			}

			return new SectorDTO()
			{
				Sector = sector,
				Sectors = subSectors
			};
		}
	}
}
