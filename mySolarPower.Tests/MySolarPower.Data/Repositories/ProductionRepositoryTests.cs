using MySolarPower.Data.Models;
using Moq;
using MockQueryable.Moq;
using Microsoft.EntityFrameworkCore;
using MySolarPower.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace mySolarPower.Tests.MySolarPower.Data.Repositories;

public class ProductionRepositoryTests
{
    protected Mock<PowerUsageDbContext> mockContext;

    // Test data
    private readonly List<SolarPower> _records = new ()
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
        },
        new SolarPower
        {
            Id = 3,
            Date = new DateTime(2021, 1, 1),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        },
        new SolarPower
        {
            Id = 4,
            Date = new DateTime(2022, 1, 2),
            EnergyProduced = 100,
            EnergyUsed = 50,
            MaxAcpowerProduced = 10
        }
    };

    public ProductionRepositoryTests()
    {
        // Create a mock of the PowerUsageDbContext using UseInMemoryDatabase
        var options = new DbContextOptionsBuilder<PowerUsageDbContext>()
            .UseInMemoryDatabase(databaseName: "PowerUsageDb")
            .Options;
        mockContext = new Mock<PowerUsageDbContext>(options);
    }

    [Fact]
    public async Task GetProductionDataAsync_ReturnsExpectedRecords_WhenRecordsExist()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);

        // Act
        var actual = await repository.GetProductionDataAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(_records, actual);
    }

    [Fact]
    public async Task GetProductionDataAsync_ReturnsEmptyCollectionOfRecords_WhenRecordsDoesNotExists()
    {
        //Arrange
        var dummyContext = new Mock<PowerUsageDbContext>();
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(dummyContext.Object, dummyLogger.Object);

        // Act
        var result = await repository.GetProductionDataAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProductionDataByDateAsync_ReturnsExpectedRecord_WhenRecordExist()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var date = new DateTime(2021, 1, 2);
        SolarPower expected = _records.Where(x => x.Date == date).Single();

        // Act
        var actual = await repository.GetProductionDataByDayAsync(date);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetProductionDataByDateAsync_ReturnsNull_WhenRecordDoesNotExist()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var date = new DateTime(2021, 1, 3);

        // Act
        var result = await repository.GetProductionDataByDayAsync(date);

        // Assert
        Assert.Null(result);    
    }

    [Fact]
    public async Task GetProductionDataByDateAsync_ReturnsNull_WhenMultipleRecordsExist()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var date = new DateTime(2021, 1, 1);

        // Act
        var result = await repository.GetProductionDataByDayAsync(date);

        // Assert
        Assert.Null(result);    
    }

    [Fact]  
    public async Task GetProductionDataByMonthAsync_ReturnsExpectedRecords_WhenRecordsExist()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var expected = _records.Where(x => x.Date.Value.Month == 1 && x.Date.Value.Year == 2021).ToList();
        var date = new DateTime(2021, 1, 1);

        // Act
        var actual = await repository.GetProductionDataByMonthAsync(date);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetProductionDataByMonthAsync_ReturnsEmptyCollectionOfRecords_WhenRecordsDoesNotExists()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var date = new DateTime(2024, 1, 1);

        // Act
        var result = await repository.GetProductionDataByMonthAsync(date);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProductionDataByYearAsync_ReturnsExpectedRecords_WhenRecordsExist()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var expected = _records.Where(x => x.Date.Value.Year == 2021).ToList();
        var date = new DateTime(2021, 1, 1);

        // Act
        var actual = await repository.GetProductionDataByYearAsync(date);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetProductionDataByYearAsync_ReturnsEmptyCollectionOfRecords_WhenRecordsDoesNotExists()
    {
        //Arrange
        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);
        var date = new DateTime(2024, 1, 1);

        // Act
        var result = await repository.GetProductionDataByYearAsync(date);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task AddProductionDataAsync_ShouldReturnTrue_WhenAddingNewRecord_IfRecordDoesNotExist()
    {
        //Arrange
        var record = new SolarPower
        {
            Date = new DateTime(2024, 1, 1),
            EnergyProduced = (decimal?)1.0,
            EnergyUsed = (decimal?)1.0,
            MaxAcpowerProduced = (decimal?)1.0
        };

        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);

        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);

        // Act
        var result = await repository.AddProductionDataAsync(record);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddProductionDataAsync_ShouldReturnFalse_WhenAddingNewRecord_IfRecordAlreadyExists()
    {
        //Arrange
        var record = new SolarPower
        {
            Date = new DateTime(2022, 1, 2),
            EnergyProduced = (decimal?)1.0,
            EnergyUsed = (decimal?)1.0,
            MaxAcpowerProduced = (decimal?)1.0
        };

        mockContext.Setup(c => c.SolarPowers).Returns(_records.AsQueryable().BuildMockDbSet().Object);

        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);

        // Act
        var result = await repository.AddProductionDataAsync(record);

        // Assert
        Assert.False(result);
    }
}
