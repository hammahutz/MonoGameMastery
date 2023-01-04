using Microsoft.Xna.Framework;

using MonoGameMastery.GameEngine.Particles;

using mathUtil =  MonoGameMastery.GameEngine.Util.MathUtil;

namespace MonoGameMastery.VerticalShooter.Particles;

public class ExplosionParticleState : EmitterParticleState
{
    public override int MinLifeSpan => 180;
    public override int MaxLifeSpan => 240;
    public override float Velocity => 2f;
    public override float VelocityDeviation => 0f;
    public override float Acceleration => 0.999f;
    public override Vector2 Gravity => mathUtil.Down;
    public override float Opacity => 0.4f;
    public override float OpacityDeviation => 0.1f;
    public override float OpacityFadingRate => 0.92f;
    public override float Rotation => 0.0f;
    public override float RotationDeviation => 0.0f;
    public override float Scale => 0.5f;
    public override float ScaleDeviation => 0.10f;
}