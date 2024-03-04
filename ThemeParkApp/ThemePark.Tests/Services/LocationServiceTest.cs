using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ThemePark.Data.DataContext;
using ThemePark.Services.Services;

namespace ThemePark.Tests.Services;

[TestClass]
public class LocationServiceTest
{
    private DbConnection _connection = null!;
    private DbContextOptions<ThemeParkDbContext> _options = null!;
    private IThemeParkDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ThemeParkDbContext>().UseSqlite(_connection).Options;

        var config = new Mock<IConfiguration>();

        _context = new ThemeParkDbContext(_options, config.Object);
        _context?.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _connection.Dispose();
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task ReturnAllLocationsTest()
    {
        var service = new LocationService(_context);

        var records = await service.GetLocationsAsync();

        Assert.IsTrue(records.Any());
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task ReturnOneLocationByIdTest()
    {
        var service = new LocationService(_context);

        var record = await service.GetLocationAsync(1);

        Assert.IsNotNull(record);
        Assert.IsTrue(record.Id == 1);
    }
}
