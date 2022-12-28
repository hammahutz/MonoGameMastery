using Microsoft.Xna.Framework;

using MonoGameMastery.GameEngine.Util;

namespace MonoGameMastery.GameEngine.Particles;

public class ConeEmitterType : IEmitterType
{
    public Vector2 Direction { get; private set; }
    public float Spread { get; private set; }

    public ConeEmitterType(Vector2 direction, float spread)
    {
        Direction = direction;
        Spread = spread;
    }

    public Vector2 GetParticleDirection() => Direction == null ? Vector2.Zero : MathUtil.GenerateDeviationFloat(Direction.ToAngle(), Spread).ToVector();
    public Vector2 GetParticlePosition(Vector2 emitterPosition) => new Vector2(emitterPosition.X, emitterPosition.Y);
}