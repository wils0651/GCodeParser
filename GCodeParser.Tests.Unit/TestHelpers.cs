using System;

namespace GCodeParser.Tests.Unit
{
    public static class TestHelpers
    {
        // TODO: Generics?
        public static bool IsEqualWithinTolerance(double actualValue, double expectedValue, double tolerance = 0.001)
        {
            var result = Math.Abs(actualValue - expectedValue) < tolerance;

            if (!result)
            {
                throw new Exception($"Not equal: actualValue: {actualValue}; expectedValue: {expectedValue}");
            }

            return result;
        }
    }
}
