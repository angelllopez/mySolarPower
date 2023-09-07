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
        GetProductionDataByDayAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.FirstOrDefault(x => x.Date == date));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByDayAsync(testDate);

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
            x.GetProductionDataByDayAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.FirstOrDefault(x => x.Date == date));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByDayAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByMonthAsynct_ReturnsOk_WhenRecordsMatchMonth()
    {
        //Arrange
        DateTime testDate = new DateTime(2021, 1, 1);
        var mockProductionRepository = new Mock<IProductionRepository>();
        mockProductionRepository.Setup(x =>
                   x.GetProductionDataByMonthAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.Where(x => x.Date.Value.Month == date.Month));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByMonthAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByMonthAsync_ReturnsNotFound_WhenRecordsDoNotMatchMonth()
    {
        //Arrange
        DateTime testDate = new DateTime(2021, 2, 1);
        var mockProductionRepository = new Mock<IProductionRepository>();
        mockProductionRepository.Setup(x =>
                   x.GetProductionDataByMonthAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.Where(x => x.Date.Value.Month == date.Month));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByMonthAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByYearAsync_ReturnsOk_WhenRecordsMatchYear()
    {
        //Arrange
        DateTime testDate = new DateTime(2021, 1, 1);
        var mockProductionRepository = new Mock<IProductionRepository>();
        mockProductionRepository.Setup(x =>
            x.GetProductionDataByYearAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.Where(x => x.Date.Value.Year == date.Year));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByYearAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByYearAsync_ReturnsNotFound_WhenRecordsDoNotMatchYear()
    {
        //Arrange
        DateTime testDate = new DateTime(2024, 1, 1);
        var mockProductionRepository = new Mock<IProductionRepository>();
        mockProductionRepository.Setup(x =>
            x.GetProductionDataByYearAsync(It.IsAny<DateTime>()))
            .ReturnsAsync((DateTime date) => _records.Where(x => x.Date.Value.Year == date.Year));

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.GetProductionDataByYearAsync(testDate);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task PostProductionDataRecord_ReturnsOk_WhenRecordIsAdded()
    {
        //Arrange
        SolarPower testRecord = new SolarPower
        {
            Id = 3,
            Date = new DateTime(2021, 1, 3),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        };

        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
            x.AddProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.AddProductionDataAsync(testRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkResult>(actual);
    }

    [Fact]
    public async Task PostProductionDataRecord_ReturnsBadRequest_WhenRecordIsNotAdded()
    {
        //Arrange
        SolarPower testRecord = new SolarPower
        {
            Id = 3,
            Date = new DateTime(2021, 1, 3),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        };

        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
                   x.AddProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(false);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.AddProductionDataAsync(testRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<BadRequestResult>(actual);
    }

    [Fact]
    public async Task DeleteProductionDataAsync_ReturnsOk_WhenRecordIsDeleted()
    {
        //Arrange
        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
                   x.DeleteProductionDataAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.DeleteProductionDataAsync(1);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkResult>(actual);
    }

    [Fact]  
    public async Task DeleteProductionDataAsync_ReturnsBadRequest_WhenRecordIsNotDeleted()
    {
        //Arrange
        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
                          x.DeleteProductionDataAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.DeleteProductionDataAsync(1);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<BadRequestResult>(actual);
    }

    [Fact]
    public async Task UpdateProductionDataAsync_ReturnsOk_WhenRecordIsUpdated()
    {
        //Arrange
        SolarPower testRecord = new SolarPower
        {
            Id = 1,
            Date = new DateTime(2021, 1, 1),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        };

        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
            x.UpdateProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.UpdateProductionDataAsync(testRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkResult>(actual);
    }

    [Fact]
    public async Task UpdateProductionDataAsync_ReturnsBadRequest_WhenRecordIsNotUpdated()
    {
        //Arrange
        SolarPower testRecord = new SolarPower
        {
            Id = 1,
            Date = new DateTime(2021, 1, 1),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        };

        var mockProductionRepository = new Mock<IProductionRepository>();

        mockProductionRepository.Setup(x =>
                   x.UpdateProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(false);

        var controller = new ProductionController(mockProductionRepository.Object);

        // Act
        var actual = await controller.UpdateProductionDataAsync(testRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<BadRequestResult>(actual);
    }
}
