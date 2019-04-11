using GCodeParser.Intepreters;
using GCodeParser.Models;
using GCodeParser.Parsers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GCodeParser.Tests.Unit
{
    [TestFixture]
    public class InterpreterTests
    {
        [Test]
        public void GO_SimpleXMove()
        {
            // Setup
            IMachine machine = new Machine();
            machine.AccelerationX = 1;
            machine.MaxVelocityX = 10;

            double xFinal = 1.00;
            string moveParameter = "X1.00";

            ParsedCommand parsedCommand = new ParsedCommand("G0", moveParameter);

            Dictionary<char, double> moveDictionary = new Dictionary<char, double>
            {
                {'X', xFinal },
            };

            Mock<IMoveParser> parserMock = new Mock<IMoveParser>();
            parserMock.Setup(p => p.ParseMove(moveParameter)).Returns(moveDictionary);

            // Act
            G0 g0 = new G0(parserMock.Object);

            g0.InterpretGCode(parsedCommand, machine);

            // Assert
            Assert.That(machine.X, Is.EqualTo(xFinal));
            Assert.That(machine.DurationSeconds, Is.EqualTo(0.2));
        }

        [Test]
        [TestCase(1.0, 1.0, 1.0, 0.2)]
        [TestCase(0.5, 1.0, 1.0, 0.2)]
        [TestCase(0.0, -1.0, 1.0, 0.2)]
        [TestCase(-1.0, 0.5, 1.0, 0.2)]
        [TestCase(-1.0, 1.0, -1.0, 0.2)]
        [TestCase(-10.0, 10.0, -10.0, 2.0)]
        public void GO_XYZMove(double xFinal, double yFinal, double zFinal, double durationSeconds)
        {
            // Setup
            IMachine machine = new Machine();
            machine.AccelerationX = machine.AccelerationY = machine.AccelerationZ = 1;
            machine.MaxVelocityX = machine.MaxVelocityY = machine.MaxVelocityZ = 10;

            string moveParameter = $"X{xFinal} Y{yFinal} Z{zFinal}";

            ParsedCommand parsedCommand = new ParsedCommand("G0", moveParameter);

            Dictionary<char, double> moveDictionary = new Dictionary<char, double>
            {
                {'X', xFinal },
                {'Y', yFinal },
                {'Z', zFinal },
            };

            Mock<IMoveParser> parserMock = new Mock<IMoveParser>();
            parserMock.Setup(p => p.ParseMove(moveParameter)).Returns(moveDictionary);

            // Act
            G0 g0 = new G0(parserMock.Object);

            g0.InterpretGCode(parsedCommand, machine);

            // Assert
            Assert.That(machine.X, Is.EqualTo(xFinal));
            Assert.That(machine.Y, Is.EqualTo(yFinal));
            Assert.That(machine.Z, Is.EqualTo(zFinal));
            Assert.That(machine.DurationSeconds, Is.EqualTo(durationSeconds));
        }

    }
}
