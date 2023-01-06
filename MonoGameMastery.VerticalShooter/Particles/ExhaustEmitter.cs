using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.Particles;

namespace MonoGameMastery.VerticalShooter.Particles;

public class ExhaustEmitter : Emitter
{
    private const int NbParticles = 10;
    private const int MaxNbParticles = 1000;
    private static Vector2 _direction = new Vector2(0.0f, 1.0f);
    private const float Spread = 1.5f;

    public ExhaustEmitter(Texture2D texture, Vector2 position) : base(texture, position, new ExhaustParticleState(), new ConeEmitterType(_direction, Spread), NbParticles, MaxNbParticles)
    {
    }
}