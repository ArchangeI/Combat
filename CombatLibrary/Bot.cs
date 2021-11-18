using System;

namespace CombatLibrary
{
    public static class Bot
    {
        private static Array values = Enum.GetValues(typeof(HitAndBlock));
        private static Random random = new Random();

        private const int MinRange = 1;
        private const int MaxRange = 4;

        public static bool botCanDecide = true;
        public static HitAndBlock botChoise;

        public static HitAndBlock RandomChoise() => (HitAndBlock)values.GetValue(random.Next(MinRange, MaxRange));

    }
}
