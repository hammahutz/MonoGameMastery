using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Particles;

public class Particle
{
    private int _lifespan;
    private int _age;
    private Vector2 _direction;
    private Vector2 _gravity;
    private float _velocity;
    private float _acceleration;
    private float _rotation;
    private float _opacityFadingRate;

    public Vector2 Position { get; private set; }
    public float Scale { get; private set; }
    public float Opacity { get; private set; }

    public void Activate(int lifespan, Vector2 position, Vector2 direction, Vector2 gravity, float velocity, float acceleration, float rotation, float scale, float opacity, float opacityFadingRate)
    {
        _lifespan = lifespan;
        _direction = direction;
        _gravity = gravity;
        _velocity = velocity;
        _acceleration = acceleration;
        _rotation = rotation;
        _opacityFadingRate = opacityFadingRate;
        _age = 0;

        Position = position;
        Scale = scale;
        Opacity = opacity;
    }

    public bool Update(GameTime gameTime)
    {
        //TODO Make frame independent
        _velocity *= _acceleration;
        _direction *= _gravity;

        Position += _direction * _velocity;

        Opacity *= _opacityFadingRate;

        _age++;

        return _age < _lifespan;
    }
}