using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.Particles;

namespace MonoGameMastery.VerticalShooter.Particles;

public class ExplosionEmitter : Emitter
{

    private const int NbParticles = 2;
    private const int MaxNbParticles = 2000;
    private const float RADIUS = 50f;

    public ExplosionEmitter(Texture2D texture, Vector2 position) : base(texture, position, new ExplosionParticleState(), new CircleEmitterType(RADIUS), NbParticles, MaxNbParticles) { }
}