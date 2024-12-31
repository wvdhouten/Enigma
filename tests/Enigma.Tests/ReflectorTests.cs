namespace Enigma.Tests;

using System;
using Enigma.Core;
using Xunit;

public class ReflectorTests
{
    [Fact]
    public void InvalidReflector_Throws()
    {
        // Arrange
        var reflectorName = string.Empty;

        // Act
        void initializer() => Reflector.CreateKnown(reflectorName);

        // Assert
        Assert.Throws<Exception>(initializer);
    }

    [Fact]
    public void ValidReflector_SetsWiring()
    {
        // Arrange
        var expected = new[] { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 };
        var reflectorName = "B";

        // Act
        var reflector = Reflector.CreateKnown(reflectorName);

        // Assert
        Assert.Equal(expected, reflector.Wiring);
    }

    [Theory]
    [InlineData(20, 2)]
    [InlineData(0, 24)]
    public void Resolve_ReturnsMappedValue(int expected, int input)
    {
        // Arrange
        var reflectorName = "B";
        var reflector = Reflector.CreateKnown(reflectorName);

        // Act
        var result = reflector.Resolve(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
