namespace QG.Tests.M2Benchmarks
{
    using System;
    using System.Globalization;

    internal static class BenchmarkCountdownFormatter
    {
        public static string Format(float seconds)
        {
            if (seconds <= 0f)
            {
                return "0";
            }

            return Math.Floor(seconds).ToString("0", CultureInfo.InvariantCulture);
        }
    }
}
