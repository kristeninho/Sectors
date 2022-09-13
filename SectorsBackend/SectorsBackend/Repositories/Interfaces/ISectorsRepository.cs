using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;

namespace SectorsBackend.Repositories.Interfaces
{
	public interface ISectorsRepository
	{
		Task<ActionResult<List<SectorDTO>>> GetSectorsSeparatedByCategoryAsync();
	}
}