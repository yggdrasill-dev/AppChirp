using Microsoft.Extensions.DependencyInjection;

namespace AppChirp.UnitTests;

public class DependencyInjectionTests
{
    [Fact]
    public void 在DI中使用EventBus()
    {
        // Arrange
        var sp = new ServiceCollection()
            .AddAppChirp(configuration => configuration
                .RegisterEventSource<Guid>("test"))
            .BuildServiceProvider();

        var sut = sp.GetRequiredService<IEventBus>();

        // Act
        var actual = sut.GetEventPublisher<Guid>("test");

        // Assert
        Assert.NotNull(actual);
    }
}