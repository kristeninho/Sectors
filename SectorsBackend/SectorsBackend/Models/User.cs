using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SectorsBackend.Models
{
	public class User
	{
		public int Id { get; set; }
		[MaxLength(30)]
		public string Name { get; set; }
		public bool AgreedToTerms { get; set; }
		public ICollection<Sector> Sectors { get; set; }
	}
}
