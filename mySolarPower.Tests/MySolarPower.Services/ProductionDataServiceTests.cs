using Microsoft.Extensions.Logging;
using mySolarPower.Services;
using mySolarPower.Services.Contracts;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;

namespace mySolarPower.Tests.MySolarPower.Services;

public class ProductionDataServiceTests
{
    public static IEnumerable<object[]> GetSolarPowerRecord()
    {
        yield return new object[]
        {
            new SolarPower
            {
                Date = new DateTime(2020, 1, 1)
            }
        };

        yield return new object[]
        {
            new SolarPower
            {
                Date = DateTime.UtcNow.AddDays(1)
            }
        };
    }

    [Fact]
    public async Task AddProductionDataRecord_ShouldCallMethod_AddProductionDataAsync()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        mockRepo.Setup(repo => repo.AddProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.AddProductionDataRecord(new SolarPower());

        // Assert
        mockRepo.Verify(repo => repo.AddProductionDataAsync(It.IsAny<SolarPower>()), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GetSolarPowerRecord))]
    public async Task AddProductionDataRecord_ShouldNotCallMethod_AddProductionDataAsync_WhenDateIsOutOfRange(SolarPower record)
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        mockRepo.Setup(repo => repo.AddProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.AddProductionDataRecord(record);

        // Assert
        mockRepo.Verify(repo => repo.AddProductionDataAsync(It.IsAny<SolarPower>()), Times.Never);
    }

    [Fact]
    public async Task DeleteProductionDataRecord_ShouldCallMethod_DeleteProductionDataAsync()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        mockRepo.Setup(repo => repo.DeleteProductionDataAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.DeleteProductionDataRecord(1);

        // Assert
        mockRepo.Verify(repo => repo.DeleteProductionDataAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProductionDataRecord_ShouldThrowArgumentException_WhenIdIsDecimal()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        mockRepo.Setup(repo => repo.DeleteProductionDataAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.DeleteProductionDataRecord(1);
    }

    [Fact]
    public async Task DeleteProductionDataRecord_ShouldNotCallMethod_DeleteProductionDataAsync_WhenIdIsLessThanOne()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        mockRepo.Setup(repo => repo.DeleteProductionDataAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.DeleteProductionDataRecord(0);

        // Assert
        mockRepo.Verify(repo => repo.DeleteProductionDataAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task GetAllProductionData_ShouldCallMethod_GetProductionDataAsync()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        mockRepo.Setup(repo => repo.GetProductionDataAsync())
            .ReturnsAsync(new List<SolarPower>());

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.GetAllProductionData();

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataAsync(), Times.Once);
    }

    [Fact]
    public async Task GetProductionDataByDay_ShouldCallMethod_GetProductionDataByDayAsync_WhenParameterDateIsInRange()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        var date = new DateTime(2021, 1, 1);

        mockRepo.Setup(repo => repo.GetProductionDataByDayAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new SolarPower());

        var dummyLogger = new Mock<ILogger<IProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        var result = await service.GetProductionDataByDay(date);

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataByDayAsync(date), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GetSolarPowerRecord))]    
    public async Task GetProductionDataByDay_ShouldNotCallMethod_GetProductionDataByDayAsync_WhenParameterDateIsOutOfRange(SolarPower record)
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();

        mockRepo.Setup(repo => repo.GetProductionDataByDayAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new SolarPower());

        var dummyLogger = new Mock<ILogger<IProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        var result = await service.GetProductionDataByDay(record.Date);

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataByDayAsync(It.IsAny<DateTime>()), Times.Never);
    }

    [Fact]
    public async Task GetProductionDataByMonth_ShouldCallMethod_GetProductionDataByMonthAsync_WhenParameterDateIsInRange()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        var date = new DateTime(2021, 1, 1);

        mockRepo.Setup(repo => repo.GetProductionDataByMonthAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new List<SolarPower>());

        var dummyLogger = new Mock<ILogger<IProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        var result = await service.GetProductionDataByMonth(date);

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataByMonthAsync(date), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GetSolarPowerRecord))]
    public async Task GetProductionDataByMonth_ShouldNotCallMethod_GetProductionDataByMonthAsync_WhenParameterDateIsOutOfRange(SolarPower record)
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();

        mockRepo.Setup(repo => repo.GetProductionDataByMonthAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new List<SolarPower>());

        var dummyLogger = new Mock<ILogger<IProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        var result = await service.GetProductionDataByMonth((DateTime)record.Date);

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataByMonthAsync(It.IsAny<DateTime>()), Times.Never);
    }

    [Fact]
    public async Task GetProductionDataByYear_ShouldCallMethod_GetProductionDataByYearAsync_WhenParameterDateIsInRange()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        var date = new DateTime(2021, 1, 1);

        mockRepo.Setup(repo => repo.GetProductionDataByYearAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new List<SolarPower>());

        var dummyLogger = new Mock<ILogger<IProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        var result = await service.GetProductionDataByYear(date);

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataByYearAsync(date), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GetSolarPowerRecord))]
    public async Task GetProductionDataByYear_ShouldNotCallMethod_GetProductionDataByYearAsync_WhenParameterDateIsOutOfRange(SolarPower record)
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();

        mockRepo.Setup(repo => repo.GetProductionDataByYearAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new List<SolarPower>());

        var dummyLogger = new Mock<ILogger<IProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        var result = await service.GetProductionDataByYear((DateTime)record.Date);

        // Assert
        mockRepo.Verify(repo => repo.GetProductionDataByYearAsync(It.IsAny<DateTime>()), Times.Never);
    }

    [Fact]
    public async Task UpdateProductionDataRecord_ShouldCallMethod_UpdateProductionDataAsync_WhenParameterDateIsInRange()
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();
        var record = new SolarPower
        {
            Date = new DateTime(2021, 1, 1),
        };

        mockRepo.Setup(repo => repo.UpdateProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.UpdateProductionDataRecord(record);

        // Assert
        mockRepo.Verify(repo => repo.UpdateProductionDataAsync(record), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GetSolarPowerRecord))]
    public async Task UpdateProductionDataRecord_ShouldNotCallMethod_UpdateProductionDataAsync_WhenParameterDateIsOutOfRange(SolarPower record)
    {
        // Arrange
        var mockRepo = new Mock<IProductionRepository>();

        mockRepo.Setup(repo => repo.UpdateProductionDataAsync(It.IsAny<SolarPower>()))
            .ReturnsAsync(true);

        var dummyLogger = new Mock<ILogger<ProductionDataService>>();

        var service = new ProductionDataService(mockRepo.Object, dummyLogger.Object);

        // Act
        await service.UpdateProductionDataRecord(record);

        // Assert
        mockRepo.Verify(repo => repo.UpdateProductionDataAsync(It.IsAny<SolarPower>()), Times.Never);
    }
}
