namespace AppChirp.UnitTests;

public class EventBusTests
{
    [Fact]
    public void 註冊EventSource後可以取得實體()
    {
        // Arrange
        var sut = new EventBus();

        sut.RegisterEventSource<Guid>("test");

        // Act
        var actual = sut.GetEventPublisher<Guid>("test");

        // Assert
        Assert.NotNull(actual);
    }

    [Fact]
    public void 如果沒有註冊EventSource會取得Null()
    {
        // Arrange
        var sut = new EventBus();

        // Act
        var actual = sut.GetEventPublisher<Guid>("test");

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void 如果Id已經有註冊_取得型別指定不對也會取得Null()
    {
        // Arrange
        var sut = new EventBus();

        sut.RegisterEventSource<Guid>("test");

        // Act
        var actual = sut.GetEventPublisher<string>("test");

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void 取得已註冊的事件發送物件()
    {
        // Arrange
        var sut = new EventBus();

        var registerEventName = "test";

        sut.RegisterEventSource<Guid>(registerEventName);

        // Act
        var actual = sut.GetEventPublisher<Guid>(registerEventName);

        // Assert
        Assert.NotNull(actual);
    }

    [Fact]
    public void 取得已註冊的事件觀察源()
    {
        // Arrange
        var sut = new EventBus();

        var registerEventName = "test";

        sut.RegisterEventSource<Guid>(registerEventName);

        // Act
        var actual = sut.GetEventObserable<Guid>(registerEventName);

        // Assert
        Assert.NotNull(actual);
    }
}