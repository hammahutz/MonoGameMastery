using System;

using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Util
{
    public static class MathUtil
    {
        public static float ToAngle(this Vector2 direction) => MathF.Atan2(direction.Y, direction.X);
        public static Vector2 ToVector(this float direction) => new Vector2(MathF.Cos(direction), MathF.Sin(direction));

        public static float GenerateDeviationFloat(float startNumber, float deviation) => RandomNumberGenerator.NextRandom(startNumber - deviation / 2.0f, startNumber + deviation / 2.0f);
    }
}