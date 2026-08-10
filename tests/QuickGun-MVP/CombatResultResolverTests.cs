namespace QG.Tests
{
    using System;
    using QG.Enum;
    using QG.GameStateMachine;

    internal static class CombatResultResolverTests
    {
        private static int failures;

        private static void Main()
        {
            AssertEqual(ECombatResult.PlayerWin, CombatResultResolver.Resolve(3, 2), "greater player health resolves to victory");
            AssertEqual(ECombatResult.Draw, CombatResultResolver.Resolve(2, 2), "equal health resolves to draw");
            AssertEqual(ECombatResult.BotWin, CombatResultResolver.Resolve(1, 2), "lower player health resolves to lose");

            if (failures > 0)
            {
                Environment.Exit(1);
            }

            Console.WriteLine("CombatResultResolver: all 3 tests passed.");
        }

        private static void AssertEqual(ECombatResult expected, ECombatResult actual, string scenario)
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
