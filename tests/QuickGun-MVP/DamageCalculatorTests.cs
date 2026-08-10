namespace QG.Tests
{
    using System;
    using QG.Combat;

    internal static class DamageCalculatorTests
    {
        private static int failures;

        private static void Main()
        {
            AssertEqual(2, DamageCalculator.Calculate(1, 2f), "headshot doubles damage");
            AssertEqual(1, DamageCalculator.Calculate(1, 1f), "body shot preserves damage");
            AssertEqual(3, DamageCalculator.Calculate(2, 1.5f), "fractional result rounds correctly");
            AssertEqual(1, DamageCalculator.Calculate(1, 0.1f), "positive hit deals at least one");
            AssertEqual(0, DamageCalculator.Calculate(0, 2f), "zero base damage stays zero");
            AssertEqual(0, DamageCalculator.Calculate(-1, 2f), "negative base damage is rejected");
            AssertEqual(0, DamageCalculator.Calculate(1, 0f), "zero multiplier disables damage");
            AssertEqual(0, DamageCalculator.Calculate(1, -2f), "negative multiplier is rejected");
            AssertEqual(10, DamageCalculator.Calculate(10, 1f, 0f), "armor 0 does not reduce body damage");
            AssertEqual(8, DamageCalculator.Calculate(10, 1f, 0.25f), "armor 0.25 receives 75 percent damage");
            AssertEqual(2, DamageCalculator.Calculate(10, 1f, 1f), "armor above 0.8 clamps to 0.8");
            AssertEqual(10, DamageCalculator.Calculate(10, 1f, -1f), "armor below 0 clamps to 0");
            AssertEqual(15, DamageCalculator.Calculate(10, 2f, 0.25f), "headshot applies before armor");
            AssertEqual(2, DamageCalculator.Calculate(5, 2f, 0.8f), "vertical slice headshot applies max armor");
            AssertEqual(8, DamageCalculator.Calculate(10, 1f, 0.25f), "body shot applies armor");
            AssertEqual(0, DamageCalculator.Calculate(0, 2f, 0.25f), "zero base damage stays zero with armor");

            if (failures > 0)
            {
                Environment.Exit(1);
            }

            Console.WriteLine("DamageCalculator: all 16 tests passed.");
        }

        private static void AssertEqual(int expected, int actual, string scenario)
        {
            if (expected == actual)
            {
                return;
            }

            failures++;
            Console.Error.WriteLine($"FAIL: {scenario}. Expected {expected}, got {actual}.");
        }
    }
}
