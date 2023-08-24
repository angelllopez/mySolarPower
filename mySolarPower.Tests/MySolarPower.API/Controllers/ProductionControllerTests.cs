using Microsoft.AspNetCore.Mvc;
using Moq;
using mySolarPower.API.Controllers;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;
using MySolarPower.Data.Repositories;

namespace mySolarPower.Tests.MySolarPower.API.Controllers;

public class ProductionControllerTests
{
    private readonly List<SolarPower> _records = new List<SolarPower>
    {
        new SolarPower
        {
            Id = 1,
            Date = DateTime.Now,
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        },
        new SolarPower
        {
            Id = 2,
            Date = DateTime.Now,
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        }
    };

    [Fact]
    public async Task GetProductionDataAsync_ReturnsOk_WhenRecordsExist()
    {
        //Arrange
        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
        x.GetProductionDataAsync())
            .ReturnsAsync(_records);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }
    [Fact]
    public async Task GetProductionDataAsync_ReturnsNotFound_WhenRecordsDoNotExist()
    {
        //Arrange
        var controller = new ProductionController(new ProductionRepository());

        // Act
        var actual = await controller.GetProductionDataAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }
}
