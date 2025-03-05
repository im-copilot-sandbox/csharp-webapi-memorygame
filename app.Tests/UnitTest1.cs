namespace app.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        int expected = 5;
        int actual = 2 + 3;

        // Act & Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(3, 3, 6)]
    public void TestAddition(int a, int b, int expected)
    {
        // Act
        int actual = a + b;

        // Assert
        Assert.Equal(expected, actual);
    }
}
