using System;

namespace MonoGameMastery.GameEngine.Util
{
    public static class RandomNumberGenerator
    {
        private static readonly Random _rnd = new Random();

        public static void NextRandom() => _rnd.Next();

        public static int NextRandom(int maxValue) => _rnd.Next(maxValue);

        public static int NextRandom(int minValue, int maxValue) => _rnd.Next(minValue, maxValue);

        public static float NextRandom(float max) => (float)_rnd.NextDouble() * max;

        public static float NextRandom(float minValue, float maxValue) => ((float)_rnd.NextDouble() * (maxValue - minValue)) + minValue;
    }
}