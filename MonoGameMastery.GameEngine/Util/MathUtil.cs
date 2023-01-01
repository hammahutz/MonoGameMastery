using System;
using System.Runtime.CompilerServices;

using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Util
{
    public static class MathUtil
    {
        public static float ToAngle(this Vector2 direction) => MathF.Atan2(direction.Y, direction.X);
        public static Vector2 ToVector(this float direction) => new Vector2(MathF.Cos(direction), MathF.Sin(direction));

        public static float GenerateDeviationFloat(float startNumber, float deviation) => RandomNumberGenerator.NextRandom(startNumber - deviation / 2.0f, startNumber + deviation / 2.0f);

        public static Vector2 Origin(this Rectangle rectangle) => new Vector2(rectangle.Width / 2, rectangle.Height / 2);
        public static PointF SizeF(this Rectangle rectangle) => new PointF(rectangle.Width / 2.0f, rectangle.Height / 2.0f);

    }
}