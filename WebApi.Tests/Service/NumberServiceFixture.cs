using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Service;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace WebApi.Tests.Service;

public class NumberServiceFixture
{
    public INumberService NumberService { get; set; }
    public Mock<IDbService> MockDbService { get; set; }
    public Mock<ILogger<NumberService>> MockLogger { get; set; }

    public NumberServiceFixture()
    {
        MockDbService = new Mock<IDbService>();
        MockLogger = new Mock<ILogger<NumberService>>();

        NumberService = new NumberService(MockDbService.Object, MockLogger.Object);
    }
}
