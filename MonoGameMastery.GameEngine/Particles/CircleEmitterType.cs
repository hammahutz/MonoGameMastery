using Microsoft.Xna.Framework;

using MonoGameMastery.GameEngine.Util;

using static MonoGameMastery.GameEngine.Util.RandomNumberGenerator;

namespace MonoGameMastery.GameEngine.Particles;

public class CircleEmitterType : IEmitterType
{
    public float Radius { get; private set; }

    public CircleEmitterType(float radius) => Radius = radius;

    public Vector2 GetParticleDirection() => Vector2.Zero;

    public Vector2 GetParticlePosition(Vector2 emitterPosition)
    {
        var newAngle = NextRandom(0, MathHelper.TwoPi);
        var positionVector = newAngle.ToVector();
        var distance = NextRandom(0, Radius);
        var position = positionVector * distance;
        var x = emitterPosition.X + position.X;
        var y = emitterPosition.Y + position.Y;

        positionVector.Normalize();

        return new(x, y);
    }
}