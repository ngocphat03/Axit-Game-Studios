namespace QG.Tests
{
    using System;
    using QG.Service;
    using UnityEngine;

    internal static class AudioSourcePoolTests
    {
        private static int failures;

        private static void Main()
        {
            ConsecutiveEmptyPoolLeasesAreDistinct();
            ReleasedSourceIsReusable();

            if (failures > 0)
            {
                Environment.Exit(1);
            }

            Console.WriteLine("AudioSourcePool: all 2 tests passed.");
        }

        private static void ConsecutiveEmptyPoolLeasesAreDistinct()
        {
            var pool = CreateEmptyPool();

            AudioSource first = pool.Get();
            AudioSource second = pool.Get();

            AssertNotSame(first, second, "consecutive empty-pool leases are distinct");
        }

        private static void ReleasedSourceIsReusable()
        {
            var pool = CreateEmptyPool();
            AudioSource released = pool.Get();
            AudioSource stillLeased = pool.Get();

            pool.Release(released);
            AudioSource reused = pool.Get();

            AssertSame(released, reused, "a released source is reused");
            AssertNotSame(stillLeased, reused, "reuse does not alias an unreleased lease");
        }

        private static AudioSourcePool CreateEmptyPool()
        {
            return new AudioSourcePool(new GameObject("AudioRoot").transform, 0);
        }

        private static void AssertSame(object expected, object actual, string scenario)
        {
            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            failures++;
            Console.Error.WriteLine($"FAIL: {scenario}. Expected the same instance.");
        }

        private static void AssertNotSame(object first, object second, string scenario)
        {
            if (!ReferenceEquals(first, second))
            {
                return;
            }

            failures++;
            Console.Error.WriteLine($"FAIL: {scenario}. Expected distinct instances.");
        }
    }
}
