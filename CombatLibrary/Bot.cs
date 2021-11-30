using System;

namespace CombatLibrary
{
    public class Bot
    {
        private static Array values = Enum.GetValues(typeof(HitAndBlock));
        private static Random random = new Random();
        private const int MinRange = 1;
        private const int MaxRange = 4;

        public readonly string Name = "Bot";

        public static HitAndBlock BotChoise() => (HitAndBlock)values.GetValue(random.Next(MinRange, MaxRange));

    }
}
