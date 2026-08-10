namespace QG.Tests.M2Benchmarks
{
    using System;
    using System.Globalization;

    internal static class BenchmarkCountdownFormatterTests
    {
        private static int failures;

        private static void Main()
        {
            CultureInfo originalCulture = CultureInfo.CurrentCulture;

            try
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");

                AssertEqual("0", BenchmarkCountdownFormatter.Format(-3.5f), "negative input clamps to zero");
                AssertEqual("0", BenchmarkCountdownFormatter.Format(0f), "zero stays zero");
                AssertEqual("0", BenchmarkCountdownFormatter.Format(0.9f), "positive fraction below one floors to zero");
                AssertEqual("30", BenchmarkCountdownFormatter.Format(30f), "exact positive value remains whole");
                AssertEqual("5", BenchmarkCountdownFormatter.Format(5.9f), "fractional positive value floors");
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }

            if (failures > 0)
            {
                Environment.Exit(1);
            }

            Console.WriteLine("BenchmarkCountdownFormatter: all 5 tests passed.");
        }

        private static void AssertEqual(string expected, string actual, string scenario)
        {
            if (string.Equals(expected, actual, StringComparison.Ordinal))
            {
                return;
            }

            failures++;
            Console.Error.WriteLine($"FAIL: {scenario}. Expected '{expected}', got '{actual}'.");
        }
    }
}
