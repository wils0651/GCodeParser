using GCodeParser.Models;
using GCodeParser.Parsers;
using NUnit.Framework;

namespace GCodeParser.Tests.Unit
{
    [TestFixture]
    public class ParserTests
    {
        private const float zero = 0.0F;

        [Test]
        public void RectilinearParser_ValidString_Success()
        {
            // Setup
            string parameterString = " X2.4051 Y0.465";

            IRectilinearParser parser = new RectilinearParser();

            // Act
            RectilinearMove result = parser.ParseMove(parameterString);

            // Assert
            Assert.That(result.X, Is.EqualTo(2.4051F));
            Assert.That(result.Y, Is.EqualTo(0.465F));
            Assert.That(result.Z, Is.EqualTo(zero));
            Assert.That(result.Speed, Is.EqualTo(zero));
        }

        [Test]
        public void RectilinearParser_ValidString2_Success()
        {
            // Setup
            string parameterString = " Z0.25";

            IRectilinearParser parser = new RectilinearParser();

            // Act
            RectilinearMove result = parser.ParseMove(parameterString);

            // Assert
            Assert.That(result.X, Is.EqualTo(zero));
            Assert.That(result.Y, Is.EqualTo(zero));
            Assert.That(result.Z, Is.EqualTo(0.25F));
            Assert.That(result.Speed, Is.EqualTo(zero));
        }

        [Test]
        public void RectilinearParser_ValidString3_Success()
        {
            // Setup
            string parameterString = "Z-0.05 F8";

            IRectilinearParser parser = new RectilinearParser();

            // Act
            RectilinearMove result = parser.ParseMove(parameterString);

            // Assert
            Assert.That(result.X, Is.EqualTo(zero));
            Assert.That(result.Y, Is.EqualTo(zero));
            Assert.That(result.Z, Is.EqualTo(-0.05F));
            Assert.That(result.Speed, Is.EqualTo(8.0F));
        }
    }
}
