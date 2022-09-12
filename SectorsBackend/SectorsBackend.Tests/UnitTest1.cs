using Moq;
using SectorsBackend.Controllers;
using SectorsBackend.DTOs;
using SectorsBackend.Models;
using SectorsBackend.Repositories;

namespace SectorsBackend.UnitTests
{   
    public class UnitTestController
    {
        private readonly Mock<SectorsRepository> sectorsRepository;
        public UnitTestController()
        {
            sectorsRepository = new Mock<SectorsRepository>();
        }

        [Fact]
        public void GetAllSectorDTOsTest()
        {
            //arrange
            var sectorsList = GetSectorDTOsTestData();

            sectorsRepository.Setup(x=>x.GetSectorsFilteredByCategoryAsync())
                .ReturnsAsync(sectorsList);

            var sectorsController = new SectorsController(sectorsRepository.Object);

            //act
            var sectorResult = sectorsController.GetAllSectorsSeperatedByCategories().Result.Value;

            //assert
            Assert.NotNull(sectorResult);
            Assert.Equal(GetSectorDTOsTestData().Count(), sectorResult.Count());
            Assert.Equal(GetSectorDTOsTestData(), sectorResult);
            Assert.True(sectorResult.Equals(sectorResult));
        }

        private List<SectorDTO> GetSectorDTOsTestData()
        {
            var sectorDTOList = new List<SectorDTO>()
            {
              new SectorDTO
              {
                  Sector = new Sector
                  {
                      Id = 1,
                      Name = "Sector1",
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
                      Id = 2,
                      Name = "Sector2",
                      HasSubSectors = false,
                      ParentId = null,
                      Users = null

                  },
                  Sectors = null
              },
            };
            
            return sectorDTOList;
        }

    }
}