namespace Enigma.Tests
{
    using Enigma.Core;
    using Xunit;

    public class PlugboardTests
    {
        [Fact]
        public void NoConnections_SetsDefaultWiring()
        {
            // Arrange
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            var connections = string.Empty;

            // Act
            var plugboard = Plugboard.Create(connections);

            // Assert
            Assert.Equal(expected, plugboard.Wiring);
        }

        [Fact]
        public void DuplicateConnections_SetsDefaultWiring()
        {
            // Arrange
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            var connections = "AB AC";

            // Act
            var plugboard = Plugboard.Create(connections);

            // Assert
            Assert.Equal(expected, plugboard.Wiring);
        }

        [Fact]
        public void IncompletePair_SetsDefaultWiring()
        {
            // Arrange
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            var connections = "AB C";

            // Act
            var plugboard = Plugboard.Create(connections);

            // Assert
            Assert.Equal(expected, plugboard.Wiring);
        }

        [Fact]
        public void InvalidPair_SetsDefaultWiring()
        {
            // Arrange
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            var connections = "AB C5";

            // Act
            var plugboard = Plugboard.Create(connections);

            // Assert
            Assert.Equal(expected, plugboard.Wiring);
        }

        [Fact]
        public void ValidConnections_SetsPluggedWiring()
        {
            // Arrange
            var expected = new[] { 25, 1, 23, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 2, 24, 0 };
            var connections = "AZ CX";

            // Act
            var plugboard = Plugboard.Create(connections);

            // Assert
            Assert.Equal(expected, plugboard.Wiring);
        }

        [Theory]
        [InlineData(23, 2)]
        [InlineData(8, 8)]
        public void Resolve_ReturnsPairedValue(int expected, int input)
        {
            // Arrange
            var connections = "CX";
            var plugboard = Plugboard.Create(connections);

            // Act
            var result = plugboard.Resolve(input); // A

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
