using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SectorsBackend.Controllers;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories.Interfaces;

namespace SectorsBackend.UnitTests.ControllersTests
{
    public class SectorsControllerTests
    {
        private readonly Mock<ISectorsRepository> sectorsRepository;
        public SectorsControllerTests()
        {
            sectorsRepository = new Mock<ISectorsRepository>();
        }

        [Fact]
        public void GetAllSectorsSeparatedByCategoriesTest_WithSectorsData()
        {
            //arrange
            var sectorsList = GetSectorDTOsTestData();

            sectorsRepository.Setup(x => x.GetSectorsSeparatedByCategoryAsync())
                .ReturnsAsync(sectorsList);

            var sectorsController = new SectorsController(sectorsRepository.Object);

            //act
            var sectorResult = sectorsController.GetAllSectorsSeparatedByCategories().Result.Value;

            //assert
            Assert.NotNull(sectorResult);
            Assert.Equal(sectorsList.Count(), sectorResult.Count());
            Assert.True(sectorsList.Equals(sectorResult));
        }

        [Fact]
        public void GetAllSectorsSeparatedByCategoriesTest_WithoutSectorsData()
        {
            //arrange
            var sectorsList = new List<SectorDTO>();

            sectorsRepository.Setup(x => x.GetSectorsSeparatedByCategoryAsync())
                .ReturnsAsync(sectorsList);

            var sectorsController = new SectorsController(sectorsRepository.Object);

            //act
            var sectorResult = sectorsController.GetAllSectorsSeparatedByCategories().Result;

            //assert
            var notFoundSectorResult = sectorResult.Result as NotFoundObjectResult;

            Assert.Null(sectorResult.Value);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundSectorResult.StatusCode);
            Assert.Equal("Sectors not found", notFoundSectorResult.Value);
        }

        private List<SectorDTO> GetSectorDTOsTestData()
        {
            var sectorDTOsList = new List<SectorDTO>()
            {
              new SectorDTO
              {
                  Sector = new Sector
                  {
                      Id = 1,
                      Name = "Sector1",
                      HasSubSectors = true,
                      ParentId = null,
                      Users = null

                  },
                  Sectors = new List<SectorDTO>()
				  {
                      new SectorDTO
					  {
                          Sector = new Sector
						  {
							Id = 4,
							Name = "Sector4",
							HasSubSectors = false,
							ParentId = 1,
							Users = null
						  },
                          Sectors = null
					  },
                      new SectorDTO
                      {
                          Sector = new Sector
                          {
                            Id = 5,
                            Name = "Sector5",
                            HasSubSectors = true,
                            ParentId = 1,
                            Users = null
                          },
                          Sectors = new List<SectorDTO>()
                              {
                                  new SectorDTO
                                  {
                                      Sector = new Sector
                                      {
                                        Id = 6,
                                        Name = "Sector6",
                                        HasSubSectors = false,
                                        ParentId = 5,
                                        Users = null
                                      },
                                      Sectors = null
                                  },
                                  new SectorDTO
                                  {
                                      Sector = new Sector
                                      {
                                        Id = 7,
                                        Name = "Sector7",
                                        HasSubSectors = false,
                                        ParentId = 5,
                                        Users = null
                                      },
                                      Sectors = null
                                  }
                              }
                      }
                  }
              },
              new SectorDTO
              {
                  Sector = new Sector
                  {
                      Id = 2,
                      Name = "Sector2",
                      HasSubSectors = false,
                      ParentId = null,
                      Users = null

                  },
                  Sectors = null
              },
              new SectorDTO
              {
                  Sector = new Sector
                  {
                      Id = 3,
                      Name = "Sector3",
                      HasSubSectors = false,
                      ParentId = null,
                      Users = null

                  },
                  Sectors = null
              },
            };
            return sectorDTOsList;
        }

    }
}