using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Particles;

namespace MonoGameMastery.GameEngine.Objects;

public class Emitter : BaseGameObject
{
    private readonly LinkedList<Particle> _activeParticles = new LinkedList<Particle>();
    private readonly LinkedList<Particle> _inactiveParticles = new LinkedList<Particle>();

    private readonly EmitterParticleState _emitterParticleState;
    private readonly IEmitterType _emitterType;

    private readonly int _particlesEmittedPerUpdate = 0;
    private readonly int _maxNbParticles = 0;
    private bool _active = true;
    public int Age { get; private set; } = 0;

    public int ActiveParticles { get => _activeParticles.Count; }
    public int InactiveParticles { get => _inactiveParticles.Count; }

    public Emitter(Texture2D texture, Vector2 position, EmitterParticleState particleState, IEmitterType emitterType, int particleEmittedPerUpdate, int maxNbParticles) : base(texture)
    {
        Position = position;
        _emitterParticleState = particleState;
        _emitterType = emitterType;
        _particlesEmittedPerUpdate = particleEmittedPerUpdate;
        _maxNbParticles = maxNbParticles;
    }

    public void Deactivate()
    {
        _active = false;
    }

    public override void Update(GameTime gameTime)
    {
        if (_active)
        {
            EmitParticles();
        }

        var particleNode = _activeParticles.First;
        while (particleNode != null)
        {
            var nextNode = particleNode.Next;
            var stillAlive = particleNode.Value.Update(gameTime);

            if (!stillAlive)
            {
                _activeParticles.Remove(particleNode);
                _inactiveParticles.AddLast(particleNode.Value);
            }

            particleNode = nextNode;
        }
        Age++;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (var particle in _activeParticles)
        {
            spriteBatch.Draw
            (
                _texture2D,
                particle.Position,
                new Rectangle(0, 0, _texture2D.Width, _texture2D.Height),
                Color.White * particle.Opacity,
                0.0f,
                Vector2.Zero,
                particle.Scale,
                SpriteEffects.None, ZIndex
            );
        }
    }

    private void EmitNewParticle(Particle particle)
    {
        var lifespan = _emitterParticleState.GenerateLifeSpan();
        var velocity = _emitterParticleState.GenerateVelocity();
        var scale = _emitterParticleState.GenerateScale();
        var rotation = _emitterParticleState.GenerateRotation();
        var opacity = _emitterParticleState.GenerateOpacity();
        var gravity = _emitterParticleState.Gravity;
        var acceleration = _emitterParticleState.Acceleration;
        var opacityFadingRate = _emitterParticleState.OpacityFadingRate;

        var direction = _emitterType.GetParticleDirection();
        var position = _emitterType.GetParticlePosition(_position);

        particle.Activate(lifespan, position, direction, gravity, velocity, acceleration, rotation, scale, opacity, opacityFadingRate);
        _activeParticles.AddLast(particle);
    }

    private void EmitParticles()
    {
        if (_activeParticles.Count >= _maxNbParticles)
        {
            return;
        }

        int maxAmountThatCanBeCreated = _maxNbParticles - _activeParticles.Count;
        int neededParticles = Math.Min(maxAmountThatCanBeCreated, _particlesEmittedPerUpdate);

        int nbToReuse = Math.Min(_inactiveParticles.Count, neededParticles);
        int nbToCreate = neededParticles - nbToReuse;

        //Reuse the inactive particles first, before creating new ones
        for (int i = 0; i < nbToReuse; i++)
        {
            var particleNode = _inactiveParticles.First;
            EmitNewParticle(particleNode.Value);
            _inactiveParticles.Remove(particleNode);
        }

        //Creating new particles
        for (int i = 0; i < nbToCreate; i++)
        {
            EmitNewParticle(new Particle());
        }
    }
}