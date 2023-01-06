using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Particles;

public interface IEmitterType
{
    Vector2 GetParticleDirection();

    Vector2 GetParticlePosition(Vector2 emitterPosition);
}