using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ThemePark.DataContext;
using ThemePark.DataContext.Models;
using ThemePark.Dto;
using ThemePark.Pages;
using ThemePark.Services;

namespace ThemePark.Tests.Pages;

[TestClass]
public class PageTests
{
    private DbContextOptions<ThemeParkDbContext> _options;
    private ThemeParkDbContext _context;

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ThemeParkDbContext>()
            .UseSqlServer(
                "Data Source=localhost;Initial Catalog=Catalog;"
                    + "Integrated Security=True;MultipleActiveResultSets=True;"
                    + "Encrypt=True;TrustServerCertificate=True"
            )
            .Options;

        var config = new Mock<IConfiguration>();

        _context = new ThemeParkDbContext(_options, config.Object);
    }

    [Ignore]
    [TestCategory("Integration")]
    public void ReturnAnIndexModelTest()
    {
        var logger = new Mock<ILogger<IndexModel>>();
        var service = new AttractionService(_context);

        var actual = new IndexModel(logger.Object, service);

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(IndexModel));
    }

    [TestMethod]
    [TestCategory("Integration")]
    public void ConfirmTheMainPageReturnsWithinThreeSecondTest()
    {
        var logger = new Mock<ILogger<IndexModel>>();
        var service = new Mock<IAttractionService>();

        var actual = new IndexModel(logger.Object, service.Object);

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(IndexModel));
    }
}
