using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Util;

public struct PointF
{
    float X { get; set; }
    float Y { get; set; }

    public PointF()
    {
        X = 0.0f;
        Y = 0.0f;
    }

    public PointF(float x, float y)
    {
        X = x;
        Y = y;
    }

    public PointF(float value) => X = Y = value;

    public Vector2 ToVector2() => new((int)X, (int)Y);
}