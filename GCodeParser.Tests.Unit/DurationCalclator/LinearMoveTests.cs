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
        [TestCase(-5.0, 5.0, 1.0, 0.0)]
        [TestCase(5.0, -5.0, -1.0, 0.0)]
        public void LinearMove_GetDistanceWhileAccerlerating(double initialVelocity, double finalVelocity, double acceleration, double expectedDistance)
        {
            var resultDistance = LinearMoves.GetDistanceWhileAccerlerating(initialVelocity, finalVelocity, acceleration);

            Assert.That(resultDistance, Is.EqualTo(expectedDistance));
        }

        [Test]
        [TestCase(0.0, 1.0, 0.0, 0.0)]
        [TestCase(0.0, 10.0, 10.0, 2.0)]
        [TestCase(1.0, 1.0, 1.0, 1.0)]
        [TestCase(-1.0, 2.0, 10.0, 20.0)]
        [TestCase(0.0, -2.0, -5.0, 5.0)]
        public void LinearMove_GetTimeWhileAccelerating(double initialVelocity, double finalVelocity, double distance, double expectedTime)
        {
            var resultTime = LinearMoves.GetTimeWhileAcceleratingToVelocity(initialVelocity, finalVelocity, distance);

            Assert.That(resultTime, Is.EqualTo(expectedTime));
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(1.0, 1.0, 1.0)]
        [TestCase(1.0, 10.0, 10.0)]
        [TestCase(10.0, 10.0, 1.0)]
        [TestCase(-1.0, -1.0, 1.0)]
        [TestCase(-1.0, -10.0, 10.0)]
        [TestCase(-10.0, -10.0, 1.0)]
        public void LinearMove_GetTimeConstantVelocity(double velocity, double distance, double expectedTime)
        {
            var resultTime = LinearMoves.GetTimeConstantVelocity(velocity, distance);

            Assert.That(resultTime, Is.EqualTo(expectedTime));
        }

        [Test]
        [TestCase(-1.0, 1.0)]
        [TestCase(1.0, -1.0)]
        public void LinearMove_GetTimeConstantVelocity_OppositeSigns_Throws(double velocity, double distance)
        {
            Assert.Throws<ArgumentException>(() => { LinearMoves.GetTimeConstantVelocity(velocity, distance); } );
        }

        [Test]
        [TestCase(0.0, 0.0, 0.0, 0.0)]
        [TestCase(0.0, 1.0, 1.0, 1.414)]
        [TestCase(1.0, 1.0, 1.0, 1.732)]
        [TestCase(0.0, 10.0, 1.0, 4.472)]
        [TestCase(10.0, 10.0, -1.0, 8.944)]
        [TestCase(5.0, 5.0, 5.0, 8.660)]
        public void LinearMove_GetMaxVelocityOverDistance(double initialVelocity, double distance, double acceleration, double expectedVelocity)
        {
            var resultVelocity = LinearMoves.GetMaxVelocityOverDistance(initialVelocity, distance, acceleration);

            Assert.IsTrue(TestHelpers.IsEqualWithinTolerance(resultVelocity, expectedVelocity));
        }

        // TODO: More test cases
        [Test]
        [TestCase(0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
        [TestCase(0.0, 10.0, 10.0, 100.0, 0.0001, 0.0)] // Distance below threshold
        [TestCase(1.0, 0.0, 5.0, 0.0, 1.0, 0.0)]
        [TestCase(1.0, 5.0, 5.0, 10.0, 1.0, 0.0)]
        [TestCase(0.0, 1.0, 5.0, 0.0, 10.0, 10.0)] //
        [TestCase(0.0, 0.0, 5.0, 1.0, 10.0, 1.0)] //
        [TestCase(0.0, 0.0, 100.0, 1.0, 1.0, 1.0)] //
        public void LinearMove_LinearMoveTime(
            double currentLocation,
            double currentVelocity,
            double maxSpeed,
            double accelerationRate,
            double endLocation, 
            double expectedTime)
        {
            var resultTime = LinearMoves.LinearMoveTime(
             currentLocation,
             currentVelocity,
             maxSpeed,
             accelerationRate,
             endLocation);

            Assert.IsTrue(TestHelpers.IsEqualWithinTolerance(resultTime, expectedTime));
        }
    }
}
