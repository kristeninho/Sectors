using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SectorsBackend.Models
{
	public class Sector
	{
		public int Id { get; set; }
		[MaxLength(60)]
		public string Name { get; set; }
		public int? ParentId { get; set; }
		public Boolean HasSubSectors { get; set; }

		[JsonIgnore]
		public ICollection<User>? Users { get; set; }
	}
}
