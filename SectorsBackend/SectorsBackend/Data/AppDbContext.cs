using Microsoft.EntityFrameworkCore;
using SectorsBackend.Models;

namespace SectorsBackend.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Sector> Sectors { get; set; }

		protected override void OnModelCreating(ModelBuilder mb)
		{
			mb.Entity<Sector>()
				.ToTable("Sector")
				.HasData(GetInitialSectors());

			mb.Entity<User>()
				.ToTable("User")
				.HasMany(user => user.Sectors)
				.WithMany(sector => sector.Users)
				.UsingEntity(joinedTable => joinedTable.ToTable("UserSector"));
		}

		private Sector[] GetInitialSectors()
		{
			return new Sector[]
			{
				// Level 0 sectors
				new Sector
				{
					Id = 1,
					Name = "Manufactoring",
					HasSubSectors = true
				},
				new Sector
				{
					Id = 2,
					Name = "Service",
					HasSubSectors = true
				},
				new Sector
				{
					Id = 3,
					Name = "Other",

					HasSubSectors = true
				},

				// Level 1 sectors
				
				// Manufactoring Level 1 sectors (id=1)
				new Sector
				{
					Id = 4,
					Name = "Construction materials",

					HasSubSectors = false,
					ParentId = 1
				},
				new Sector
				{
					Id = 5,
					Name = "Electronics and Optics",

					HasSubSectors = false,
					ParentId = 1
				},
				new Sector
				{
					Id = 6,
					Name = "Food and Beverage",

					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 7,
					Name = "Furniture",

					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 8,
					Name = "Machinery",

					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 9,
					Name = "Metalworking",
					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 10,
					Name = "Plastic and Rubber",
					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 11,
					Name = "Printing",
					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 12,
					Name = "Textile and Clothing",
					HasSubSectors = true,
					ParentId = 1
				},
				new Sector
				{
					Id = 13,
					Name = "Wood",
					HasSubSectors = true,
					ParentId = 1
				},

				// Service Level 1 sectors (id=2)
				new Sector
				{
					Id = 14,
					Name = "Business services",
					HasSubSectors = false,
					ParentId = 2
				},
				new Sector
				{
					Id = 15,
					Name = "Engineering",
					HasSubSectors = false,
					ParentId = 2
				},
				new Sector
				{
					Id = 16,
					Name = "Information Technology and Telecommunications",
					HasSubSectors = true,
					ParentId = 2
				},
				new Sector
				{
					Id = 17,
					Name = "Tourism",
					HasSubSectors = false,
					ParentId = 2
				},
				new Sector
				{
					Id = 18,
					Name = "Translation services",
					HasSubSectors = false,
					ParentId = 2
				},
				new Sector
				{
					Id = 19,
					Name = "Transport and Logistics",
					HasSubSectors = true,
					ParentId = 2
				},

				// Other Level 1 sectors (id=3)
				new Sector
				{
					Id = 20,
					Name = "Creative industries",
					HasSubSectors = false,
					ParentId = 3
				},
				new Sector
				{
					Id = 21,
					Name = "Energy technology",
					HasSubSectors = false,
					ParentId = 3
				},
				new Sector
				{
					Id = 22,
					Name = "Environment",
					HasSubSectors = false,
					ParentId = 3
				},

				//Level 2 sectors
				//Manufactoring Level 2 sectors
				
				//Food and beverage Level 2 sectors (id=6)
				new Sector
				{
					Id = 23,
					Name = "Milk & dairy products",
					HasSubSectors = false,
					ParentId = 6
				},
				new Sector
				{
					Id = 24,
					Name = "Meat & meat products",
					HasSubSectors = false,
					ParentId = 6
				},
				new Sector
				{
					Id = 25,
					Name = "Fish & fish products",
					HasSubSectors = false,
					ParentId = 6
				},
				new Sector
				{
					Id = 26,
					Name = "Bakery & confectionery products",
					HasSubSectors = false,
					ParentId = 6
				},
				new Sector
				{
					Id = 27,
					Name = "Sweets & snack food",
					HasSubSectors = false,
					ParentId = 6
				},
				new Sector
				{
					Id = 28,
					Name = "Beverages",
					HasSubSectors = false,
					ParentId = 6
				},
				new Sector
				{
					Id = 29,
					Name = "Other",
					HasSubSectors = false,
					ParentId = 6
				},

				//Furniture Level 2 sectors (id=7)
				new Sector
				{
					Id = 30,
					Name = "Office",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 31,
					Name = "Bedroom",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 32,
					Name = "Living room",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 33,
					Name = "Children's room",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 34,
					Name = "Kitchen",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 35,
					Name = "Bathroom/sauna",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 36,
					Name = "Outdoor",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 37,
					Name = "Project furniture",
					HasSubSectors = false,
					ParentId = 7
				},
				new Sector
				{
					Id = 38,
					Name = "Other",
					HasSubSectors = false,
					ParentId = 7
				},

				//Machinery Level 2 sectors (id=8)
				new Sector
				{
					Id = 39,
					Name = "Machinery components",
					HasSubSectors = false,
					ParentId = 8
				},
				new Sector
				{
					Id = 40,
					Name = "Machinery equipment/tools",
					HasSubSectors = false,
					ParentId = 8
				},
				new Sector
				{
					Id = 41,
					Name = "Manufacture of machinery",
					HasSubSectors = false,
					ParentId = 8
				},
				new Sector
				{
					Id = 42,
					Name = "Maritime",
					HasSubSectors = true,
					ParentId = 8
				},
				new Sector
				{
					Id = 43,
					Name = "Metal structures",
					HasSubSectors = false,
					ParentId = 8
				},
				new Sector
				{
					Id = 44,
					Name = "Repair and maintenance service",
					HasSubSectors = false,
					ParentId = 8
				},
				new Sector
				{
					Id = 45,
					Name = "Other",
					HasSubSectors = false,
					ParentId = 8
				},

				//Metalworking Level 2 sectors (id=9)
				new Sector
				{
					Id = 46,
					Name = "Construction of metal structures",
					HasSubSectors = false,
					ParentId = 9
				},
				new Sector
				{
					Id = 47,
					Name = "Houses and buildings",
					HasSubSectors = false,
					ParentId = 9
				},
				new Sector
				{
					Id = 48,
					Name = "Metal products",
					HasSubSectors = false,
					ParentId = 9
				},
				new Sector
				{
					Id = 49,
					Name = "Metal works",
					HasSubSectors = true,
					ParentId = 9
				},
				
				//Plastic and Rubber Level 2 sectors (id=10)
				new Sector
				{
					Id = 50,
					Name = "Packaging",
					HasSubSectors = false,
					ParentId = 10
				},
				new Sector
				{
					Id = 51,
					Name = "Plastic goods",
					HasSubSectors = false,
					ParentId = 10
				},
				new Sector
				{
					Id = 52,
					Name = "Plastic processing technology",
					HasSubSectors = true,
					ParentId = 10
				},
				new Sector
				{
					Id = 53,
					Name = "Plastic profiles",
					HasSubSectors = false,
					ParentId = 10
				},

				//Printing Level 2 sectors (id=11)
				new Sector
				{
					Id = 54,
					Name = "Advertising",
					HasSubSectors = false,
					ParentId = 11
				},
				new Sector
				{
					Id = 55,
					Name = "Book/Periodicals printing",
					HasSubSectors = false,
					ParentId = 11
				},
				new Sector
				{
					Id = 56,
					Name = "Labelling and packaging printing",
					HasSubSectors = false,
					ParentId = 11
				},

				//Textile and Clothing Level 2 sectors (id=12)
				new Sector
				{
					Id = 57,
					Name = "Clothing",
					HasSubSectors = false,
					ParentId = 12
				},
				new Sector
				{
					Id = 58,
					Name = "Textile",
					HasSubSectors = false,
					ParentId = 12
				},

				//Wood Level 2 sectors (id=13)
				new Sector
				{
					Id = 59,
					Name = "Wooden houses",
					HasSubSectors = false,
					ParentId = 13
				},
				new Sector
				{
					Id = 60,
					Name = "Wooden building materials",
					HasSubSectors = false,
					ParentId = 13
				},
				new Sector
				{
					Id = 61,
					Name = "Other",
					HasSubSectors = false,
					ParentId = 13
				},

				//Service Level 2 sectors

				//Information Technology and Telecommunications Level 2 sectors (id=16)
				new Sector
				{
					Id = 62,
					Name = "Data processing, Web portals, E-marketing",
					HasSubSectors = false,
					ParentId = 16
				},
				new Sector
				{
					Id = 63,
					Name = "Programming, Consultancy",
					HasSubSectors = false,
					ParentId = 16
				},
				new Sector
				{
					Id = 64,
					Name = "Software, Hardware",
					HasSubSectors = false,
					ParentId = 16
				},
				new Sector
				{
					Id = 65,
					Name = "Telecommunications",
					HasSubSectors = false,
					ParentId = 16
				},

				//Transport and Logistics Level 2 sectors (id=19)
				new Sector
				{
					Id = 66,
					Name = "Air",
					HasSubSectors = false,
					ParentId = 19
				},
				new Sector
				{
					Id = 67,
					Name = "Rail",
					HasSubSectors = false,
					ParentId = 19
				},
				new Sector
				{
					Id = 68,
					Name = "Road",
					HasSubSectors = false,
					ParentId = 19
				},
				new Sector
				{
					Id = 69,
					Name = "Water",
					HasSubSectors = false,
					ParentId = 19
				},

				//Manufactoring Level 3 sectors
				//Machinery Level 3 sectors

				//Maritime Level 3 sectors (id=42)
				new Sector
				{
					Id = 70,
					Name = "Aluminium and steel workboats",
					HasSubSectors = false,
					ParentId = 42
				},
				new Sector
				{
					Id = 71,
					Name = "Boat/Yacht building",
					HasSubSectors = false,
					ParentId = 42
				},
				new Sector
				{
					Id = 72,
					Name = "Ship repair and conversion",
					HasSubSectors = false,
					ParentId = 42
				},

				//Metalworking Level 3 sectors

				//Metal works Level 3 sectors (id=49)
				new Sector
				{
					Id = 73,
					Name = "CNC-machining",
					HasSubSectors = false,
					ParentId = 49
				},
				new Sector
				{
					Id = 74,
					Name = "Forgings, Fasteners",
					HasSubSectors = false,
					ParentId = 49
				},
				new Sector
				{
					Id = 75,
					Name = "Gas, Plasma, Laser cutting",
					HasSubSectors = false,
					ParentId = 49
				},
				new Sector
				{
					Id = 76,
					Name = "MIG, TIG, Aluminum welding",
					HasSubSectors = false,
					ParentId = 49
				},

				//Plastic and Rubber Level 3 sectors

				//Plastic processing technology Level 3 sectors (id=52)
				new Sector
				{
					Id = 77,
					Name = "Blowing",
					HasSubSectors = false,
					ParentId = 52
				},
				new Sector
				{
					Id = 78,
					Name = "Moulding",
					HasSubSectors = false,
					ParentId = 52
				},
				new Sector
				{
					Id = 79,
					Name = "Plastics welding and processing",
					HasSubSectors = false,
					ParentId = 52
				},
			};
		}
	}
}
