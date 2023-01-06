using Microsoft.Xna.Framework;

using MonoGameMastery.GameEngine.Util;

namespace MonoGameMastery.GameEngine.Particles
{
    public abstract class EmitterParticleState
    {
        public abstract int MinLifeSpan { get; }
        public abstract int MaxLifeSpan { get; }

        public abstract float Velocity { get; }
        public abstract float VelocityDeviation { get; }
        public abstract float Acceleration { get; }
        public abstract Vector2 Gravity { get; }

        public abstract float Opacity { get; }
        public abstract float OpacityDeviation { get; }
        public abstract float OpacityFadingRate { get; }

        public abstract float Rotation { get; }
        public abstract float RotationDeviation { get; }

        public abstract float Scale { get; }
        public abstract float ScaleDeviation { get; }

        protected float GenerateFloat(float startN, float deviation) => RandomNumberGenerator.NextRandom(startN - deviation / 2.0f, startN + deviation / 2.0f);

        public int GenerateLifeSpan() => RandomNumberGenerator.NextRandom(MinLifeSpan, MaxLifeSpan);

        public float GenerateVelocity() => RandomNumberGenerator.NextRandom(Velocity, VelocityDeviation);

        public float GenerateOpacity() => RandomNumberGenerator.NextRandom(Opacity, OpacityDeviation);

        public float GenerateRotation() => RandomNumberGenerator.NextRandom(Rotation, RotationDeviation);

        public float GenerateScale() => RandomNumberGenerator.NextRandom(Scale, ScaleDeviation);
    }
}