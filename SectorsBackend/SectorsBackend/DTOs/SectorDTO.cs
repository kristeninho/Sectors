using SectorsBackend.Models;

namespace SectorsBackend.DTOs
{
	public class SectorDTO
	{
		public Sector Sector { get; set; }

		public List<SectorDTO>? Sectors { get; set; }

		public SectorDTO()
		{
			Sector = new Sector();
			Sectors = new List<SectorDTO>();
		}
	}
}
