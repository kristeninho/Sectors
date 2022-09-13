using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories;
using SectorsBackend.Repositories.Interfaces;
using SectorsBackend.UnitTests.RepositoriesTests;

namespace SectorsBackend.UnitTests.RepositoriesTests
{
	public class SectorsRepositoryTests
	{
		private readonly IDbContextFactory<AppDbContext> _context;
		private readonly SectorsRepository _repository;
		public SectorsRepositoryTests()
		{
			_context = new TestDbContextFactory();
			_repository = new SectorsRepository(_context);
			InitializeSectorsDatabase(_context);
		}

		private void InitializeSectorsDatabase(IDbContextFactory<AppDbContext> context)
		{
			using var dbContext = _context.CreateDbContext();
			dbContext.Sectors.AddRange(GetSectorsTestData());
			dbContext.SaveChanges();
		}

		[Fact]
		public void GetSectorsFilteredByCategoryAsyncTest()
		{
			//arrange

			//act
			var sectorDTOsExpected = GetSectorDTOsTestData();
			var sectorDTOsActual = _repository.GetSectorsFilteredByCategoryAsync().Result.Value;
			var expectedJson = System.Text.Json.JsonSerializer.Serialize(sectorDTOsExpected);
			var actualJson = System.Text.Json.JsonSerializer.Serialize(sectorDTOsActual);

			//assert
			Assert.Equal(sectorDTOsExpected.Count, sectorDTOsActual.Count);
			Assert.Equal(expectedJson, actualJson);
		}

		private static List<SectorDTO> GetSectorDTOsTestData()
		{
			var sectorDTOsList = new List<SectorDTO>()
			{
				new SectorDTO
				{
					Sector = new Sector
							{
								Id = 1,
								Name = "Manufactoring",
								HasSubSectors = true
							},
					Sectors = new List<SectorDTO>
					{
						new SectorDTO
						{
							Sector = new Sector
							{
								Id = 3,
								Name = "Construction materials",
								HasSubSectors = false,
								ParentId = 1
							},
							Sectors = null
						},
						new SectorDTO
						{
							Sector = new Sector
							{
								Id = 4,
								Name = "Electronics and Optics",
								HasSubSectors = false,
								ParentId = 1
							},
							Sectors = null
						}
					}
				},
				new SectorDTO
				{
					Sector = new Sector
					{
						Id = 2,
						Name = "Service",
						HasSubSectors = false
					},
					Sectors = null
				}
			};
			return sectorDTOsList;
		}
		private static List<Sector> GetSectorsTestData()
		{
			var sectorsList = new List<Sector>()
			{
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
					HasSubSectors = false
				},

				// Level 1 sectors
				
				// Manufactoring Level 1 sectors (id=1)
				new Sector
				{
					Id = 3,
					Name = "Construction materials",

					HasSubSectors = false,
					ParentId = 1
				},
				new Sector
				{
					Id = 4,
					Name = "Electronics and Optics",

					HasSubSectors = false,
					ParentId = 1
				},
			};
			return sectorsList;
		}
	}
}
