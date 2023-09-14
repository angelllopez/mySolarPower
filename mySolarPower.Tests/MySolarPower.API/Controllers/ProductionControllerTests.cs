using Microsoft.AspNetCore.Mvc;
using mySolarPower.API.Controllers;
using mySolarPower.Services.Contracts;
using MySolarPower.Data.Models;

namespace mySolarPower.Tests.MySolarPower.API.Controllers;

public class ProductionControllerTests
{
    [Fact]
    [Trait("Category", "GetProductionDataAsync")]
    public async Task GetProductionDataAsync_ReturnsOk_WhenRecordsExist()
    {
        //Arrange
        var stubList = new List<SolarPower>() { new SolarPower() };
        var mockService = new Mock<IProductionDataService>(MockBehavior.Strict);

        mockService.Setup(x =>
            x.GetAllProductionData())
            .ReturnsAsync(stubList);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    [Trait("Category", "GetProductionDataAsync")]
    public async Task GetProductionDataAsync_ReturnsNotFound_WhenRecordsDoNotExist()
    {
        //Arrange
        List<SolarPower> stubList = new();
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
            x.GetAllProductionData())
            .ReturnsAsync(stubList);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByDayAsync_ReturnsOk_WhenRecordsExist()
    {
        //Arrange
        var stubRecord = new SolarPower { Id = 1 };
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
            x.GetProductionDataByDay(It.IsAny<DateTime>()))
            .ReturnsAsync(stubRecord);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataByDayAsync(new DateTime());

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByDayAsync_ReturnsNotFound_WhenRecordDoesNotExist()
    {
        //Arrange
        var stubRecord = new SolarPower();
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
            x.GetProductionDataByDay(It.IsAny<DateTime>()))
            .ReturnsAsync(stubRecord);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataByDayAsync(new DateTime());

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByMonthAsynct_ReturnsOk_WhenRecordsExist()
    {
        //Arrange
        var stubList = new List<SolarPower>() { new SolarPower() };
        var mockService = new Mock<IProductionDataService>();
        mockService.Setup(x =>
                   x.GetProductionDataByMonth(It.IsAny<DateTime>()))
            .ReturnsAsync(stubList);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataByMonthAsync(new DateTime());

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByMonthAsync_ReturnsNotFound_WhenRecordsDoNotExist()
    {
        //Arrange
        var stubList = new List<SolarPower>();
        var mockService = new Mock<IProductionDataService>();
        mockService.Setup(x =>
                   x.GetProductionDataByMonth(It.IsAny<DateTime>()))
            .ReturnsAsync(stubList);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataByMonthAsync(new DateTime());

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByYearAsync_ReturnsOk_WhenRecordsExist()
    {
        //Arrange
        var stubList = new List<SolarPower>() { new SolarPower() };
        var mockService = new Mock<IProductionDataService>();
        mockService.Setup(x =>
            x.GetProductionDataByYear(It.IsAny<DateTime>()))
            .ReturnsAsync(stubList);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataByYearAsync(new DateTime());

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkObjectResult>(actual);
    }

    [Fact]
    public async Task GetProductionDataByYearAsync_ReturnsNotFound_WhenRecordsDoNotExist()
    {
        //Arrange
        var stubList = new List<SolarPower>();
        var mockService = new Mock<IProductionDataService>();
        mockService.Setup(x =>
            x.GetProductionDataByYear(It.IsAny<DateTime>()))
            .ReturnsAsync(stubList);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.GetProductionDataByYearAsync(new DateTime());

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<NotFoundResult>(actual);
    }

    [Fact]
    public async Task PostProductionDataRecord_ReturnsOk_WhenRecordIsAdded()
    {
        //Arrange
        var dummyRecord = new SolarPower();
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
            x.AddProductionDataRecord(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.AddProductionDataAsync(dummyRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkResult>(actual);
    }

    [Fact]
    public async Task PostProductionDataRecord_ReturnsBadRequest_WhenRecordIsNotAdded()
    {
        //Arrange
        var dummyRecord = new SolarPower();
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
                   x.AddProductionDataRecord(It.IsAny<SolarPower>()))
            .ReturnsAsync(false);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.AddProductionDataAsync(dummyRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<BadRequestResult>(actual);
    }

    [Fact]
    public async Task DeleteProductionDataAsync_ReturnsOk_WhenRecordIsDeleted()
    {
        //Arrange
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
                   x.DeleteProductionDataRecord(It.IsAny<int>()))
            .ReturnsAsync(true);

        var controller = new ProductionController(mockService.Object);

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
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
                          x.DeleteProductionDataRecord(It.IsAny<int>()))
            .ReturnsAsync(false);

        var controller = new ProductionController(mockService.Object);

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
        var dummyRecord = new SolarPower();
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
            x.UpdateProductionDataRecord(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.UpdateProductionDataAsync(dummyRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<OkResult>(actual);
    }

    [Fact]
    public async Task UpdateProductionDataAsync_ReturnsBadRequest_WhenRecordIsNotUpdated()
    {
        //Arrange
        var dummyRecord = new SolarPower();
        var mockService = new Mock<IProductionDataService>();

        mockService.Setup(x =>
                   x.UpdateProductionDataRecord(It.IsAny<SolarPower>()))
            .ReturnsAsync(false);

        var controller = new ProductionController(mockService.Object);

        // Act
        var actual = await controller.UpdateProductionDataAsync(dummyRecord);

        // Assert
        Assert.NotNull(actual);
        Assert.IsType<BadRequestResult>(actual);
    }
}
