using System;
using System.Collections.Generic;

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

        public static Vector2 Right => Vector2.UnitX;
        public static Vector2 Up => Vector2.UnitY;
        public static Vector2 Left => -Vector2.UnitX;
        public static Vector2 Down => -Vector2.UnitY;

        public static Vector2 UpRight
        {
            get
            {
                Vector2 vec = new(1, -1);
                vec.Normalize();
                return vec;
            }
        }

        public static Vector2 UpLeft
        {
            get
            {
                Vector2 vec = new(-1, -1);
                vec.Normalize();
                return vec;
            }
        }

        public static Vector2 DownLeft
        {
            get
            {
                Vector2 vec = new(-1, 1);
                vec.Normalize();
                return vec;
            }
        }

        public static Vector2 DownRight
        {
            get
            {
                Vector2 vec = new(1, 1);
                vec.Normalize();
                return vec;
            }
        }
        public static Rectangle[,] SpriteSheet(Rectangle spriteSheetDimensions, Point frameSize)
        {
            Rectangle[,] spriteSheet = new Rectangle[spriteSheetDimensions.Width, spriteSheetDimensions.Height];

            for (int y = 0; y < spriteSheetDimensions.Height; y++)
            {
                for (int x = 0; x < spriteSheetDimensions.Width; x++)
                {
                    spriteSheet[x, y] = new Rectangle
                    (
                        spriteSheetDimensions.X + frameSize.X * x,
                        spriteSheetDimensions.Y + frameSize.Y * y,
                        frameSize.X,
                        frameSize.Y
                    );

                }
            }
            return spriteSheet;
        }
    }
}