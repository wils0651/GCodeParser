using GCodeParser.Models;
using GCodeParser.Parsers;
using NUnit.Framework;

namespace GCodeParser.Tests.Unit
{
    [TestFixture]
    public class ParserTests
    {
        private const double zero = 0.0;

        [Test]
        public void MoveParser_ValidString_Success()
        {
            // Setup
            string parameterString = " X2.4051 Y0.465";

            IMoveParser parser = new MoveParser();

            // Act
            var result = parser.ParseMove(parameterString);

            double x = result.TryGetValue('X', out double xResult) ? xResult : 0.0;
            double y = result.TryGetValue('Y', out double yResult) ? yResult : 0.0;
            double z = result.TryGetValue('Z', out double zResult) ? zResult : 0.0;
            double speed = result.TryGetValue('F', out double fResult) ? fResult : 0.0;

            // Assert
            Assert.That(x, Is.EqualTo(2.4051));
            Assert.That(y, Is.EqualTo(0.465));
            Assert.That(z, Is.EqualTo(zero));
            Assert.That(speed, Is.EqualTo(zero));
        }

        [Test]
        public void MoveParser_ValidString2_Success()
        {
            // Setup
            string parameterString = " Z0.25";

            IMoveParser parser = new MoveParser();

            // Act
            var result = parser.ParseMove(parameterString);

            double x = result.TryGetValue('X', out double xResult) ? xResult : 0.0;
            double y = result.TryGetValue('Y', out double yResult) ? yResult : 0.0;
            double z = result.TryGetValue('Z', out double zResult) ? zResult : 0.0;
            double speed = result.TryGetValue('F', out double fResult) ? fResult : 0.0;

            // Assert
            Assert.That(x, Is.EqualTo(zero));
            Assert.That(y, Is.EqualTo(zero));
            Assert.That(z, Is.EqualTo(0.25));
            Assert.That(speed, Is.EqualTo(zero));
        }

        [Test]
        public void MoveParser_ValidString3_Success()
        {
            // Setup
            string parameterString = "Z-0.05 F8";

            IMoveParser parser = new MoveParser();

            // Act
            var result = parser.ParseMove(parameterString);

            double x = result.TryGetValue('X', out double xResult) ? xResult : 0.0;
            double y = result.TryGetValue('Y', out double yResult) ? yResult : 0.0;
            double z = result.TryGetValue('Z', out double zResult) ? zResult : 0.0;
            double speed = result.TryGetValue('F', out double fResult) ? fResult : 0.0;

            // Assert
            Assert.That(x, Is.EqualTo(zero));
            Assert.That(y, Is.EqualTo(zero));
            Assert.That(z, Is.EqualTo(-0.05));
            Assert.That(speed, Is.EqualTo(8.0));
        }

        [Test]
        public void CommandParser_ValidString_Success()
        {
            // Setup
            string command = "G2 X2.4016 Y0.4564 I-0.1524 J0.0563 F12";

            ICommandParser parser = new CommandParser();

            // Act
            var result = parser.ParseCommand(command);

            // Assert
            Assert.That(result.GCodeCommand, Is.EqualTo("G2"));
            Assert.That(result.ParameterString, Is.EqualTo("X2.4016 Y0.4564 I-0.1524 J0.0563 F12"));
        }

        [Test]
        public void CommandParser_Comment_NOP()
        {
            // Setup
            string command = "(this is a comment)";

            ICommandParser parser = new CommandParser();

            // Act
            var result = parser.ParseCommand(command);

            // Assert
            Assert.That(result.GCodeCommand, Is.EqualTo("NOP"));
            Assert.That(result.ParameterString, Is.EqualTo("(this is a comment)"));
        }

        [Test]
        public void CommandParser_MultipleCommands_Success()
        {
            // Setup
            string command = "G20 G90 G40";

            ICommandParser parser = new CommandParser();

            // Act
            var result = parser.ParseCommand(command);

            // Assert
            Assert.That(result.GCodeCommand, Is.EqualTo("G20"));
            Assert.That(result.ParameterString, Is.EqualTo("G90 G40"));
        }

        [Test]
        public void CommandParser_LeadingZeros_Success()
        {
            // Setup
            string command = "G02 X2.4016 Y0.4564 I-0.1524 J0.0563 F12";

            ICommandParser parser = new CommandParser();

            // Act
            var result = parser.ParseCommand(command);

            // Assert
            Assert.That(result.GCodeCommand, Is.EqualTo("G2"));
            Assert.That(result.ParameterString, Is.EqualTo("X2.4016 Y0.4564 I-0.1524 J0.0563 F12"));
        }
    }
}
