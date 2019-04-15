using GCodeParser.DurationCalculator;
using NUnit.Framework;
using System;

namespace GCodeParser.Tests.Unit.DurationCalclator
{
    [TestFixture]
    class LinearMoveTests
    {
        [Test]
        [TestCase(0.0, 0.0, 1.0, 0.0)]
        [TestCase(0.0, 10.0, 1.0, 50.0)]
        [TestCase(10.0, 0.0, -1.0, 50.0)]
        public void LinearMove_GetDistanceWhileAccerlerating(double initialVelocity, double finalVelocity, double acceleration, double expectedDistance)
        {
            var resultDistance = LinearMoves.GetDistanceWhileAccerlerating(initialVelocity, finalVelocity, acceleration);

            Assert.That(resultDistance, Is.EqualTo(expectedDistance));
        }

        [Test]
        [TestCase(0.0, 1.0, 0.0, 0.0)]
        [TestCase(0.0, 10.0, 10.0, 2.0)]
        public void LinearMove_GetTimeWhileAccelerating(double initialVelocity, double finalVelocity, double distance, double expectedTime)
        {
            var resultTime = LinearMoves.GetTimeWhileAccelerating(initialVelocity, finalVelocity, distance);

            Assert.That(resultTime, Is.EqualTo(expectedTime));
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 1.0, 1.0)]
        [TestCase(1.0, 10.0, 10.0)]
        [TestCase(10.0, 10.0, 1.0)]
        public void LinearMove_GetTimeConstantVelocity(double velocity, double distance, double expectedTime)
        {
            var resultTime = LinearMoves.GetTimeConstantVelocity(velocity, distance);

            Assert.That(resultTime, Is.EqualTo(expectedTime));
        }

        [Test]
        public void LinearMove_GetTimeConstantVelocity_OppositeSigns_Throws()
        {
            double velocity = -1.0;
            double distance = 1.0;

            Assert.Throws<ArgumentException>(() => { LinearMoves.GetTimeConstantVelocity(velocity, distance); } );
        }

        // TODO: More test cases
        [Test]
        [TestCase(0.0, 0.0, 0.0, 0.0)]
        public void LinearMove_GetMaxVelocityOverDistance(double initialVelocity, double distance, double acceleration, double expectedVelocity)
        {
            var resultVelocity = LinearMoves.GetMaxVelocityOverDistance(initialVelocity, distance, acceleration);

            Assert.That(resultVelocity, Is.EqualTo(expectedVelocity));
        }

        // TODO: More test cases
        [Test]
        [TestCase(0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
        public void LinearMove_GetMaxVelocityOverDistance(
            double currentLocation,
            double currentVelocity,
            double maxVelocity,
            double acceleration,
            double endLocation, 
            double expectedVelocity)
        {
            var resultVelocity = LinearMoves.LinearMoveTime(
             currentLocation,
             currentVelocity,
             maxVelocity,
             acceleration,
             endLocation);

            Assert.That(resultVelocity, Is.EqualTo(expectedVelocity));
        }
    }
}
