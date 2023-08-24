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

    public ProductionRepositoryTests()
    {
        // Convert the test data into a mock queryable list
        var mock = _records.AsQueryable().BuildMockDbSet();

        // Create a mock of the PowerUsageDbContext using UseInMemoryDatabase
        var options = new DbContextOptionsBuilder<PowerUsageDbContext>()
            .UseInMemoryDatabase(databaseName: "PowerUsageDb")
            .Options;
        mockContext = new Mock<PowerUsageDbContext>(options);

        // Setup and map mockContext.SolarPower to the mock queryable list
        mockContext.Setup(c => c.SolarPowers).Returns(mock.Object);
    }

    [Fact]
    public async Task GetProductionDataAsync_ReturnsExpectedRecords_WhenRecordsExist()
    {
        //Arrange
        var dummyLogger = new Mock<ILogger<ProductionRepository>>();
        var repository = new ProductionRepository(mockContext.Object, dummyLogger.Object);

        // Act
        var result = await repository.GetProductionDataAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_records, result);
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
}
