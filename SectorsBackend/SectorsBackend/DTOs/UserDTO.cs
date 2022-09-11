namespace SectorsBackend.DTOs
{
	public class UserDTO
	{
		public string Name { get; set; }
		public bool AgreedToTerms { get; set; }
		public List<int> SectorIds { get; set; }
	}
}
