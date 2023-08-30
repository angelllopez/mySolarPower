using Microsoft.AspNetCore.Mvc;
using Moq;
using mySolarPower.API.Controllers;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;

namespace mySolarPower.Tests.MySolarPower.API.Controllers;

public class ProductionControllerTests
{
    private readonly List<SolarPower> _records = new List<SolarPower>
    {
        new SolarPower
        {
            Id = 1,
            Date = new DateTime(2021, 1, 1),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        },
        new SolarPower
        {
            Id = 2,
            Date = new DateTime(2021, 1, 2),
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
        List<SolarPower> emptyList = new List<SolarPower>();
        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
            x.GetProductionDataAsync())
            .ReturnsAsync(emptyList);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByDateAsync_ReturnsOk_WhenRecordMatchesDate()
    {
        //Arrange
        DateTime testDate = new DateTime(2021, 1, 1);
        Mock<IProductionRepository> mockProductionRepository = new Mock<IProductionRepository>();
        mockProductionRepository.Setup(x => x.
        GetProductionDataByDateAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.FirstOrDefault(x => x.Date == date));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByDateAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByDateAsync_ReturnsNotFound_WhenRecordDoesNotMatchDate()
    {
        //Arrange
        DateTime testDate = new DateTime(2022, 1, 1);
        var mockProductionRepository = new Mock<IProductionRepository>();
        mockProductionRepository.Setup(x =>
            x.GetProductionDataByDateAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.FirstOrDefault(x => x.Date == date));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByDateAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

}
