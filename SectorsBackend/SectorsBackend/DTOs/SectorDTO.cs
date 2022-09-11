using SectorsBackend.Models;

namespace SectorsBackend.DTOs
{
	public class SectorDTO
	{
		public Sector Sector { get; set; }

		public List<SectorDTO>? Sectors { get; set; }
	}
}
