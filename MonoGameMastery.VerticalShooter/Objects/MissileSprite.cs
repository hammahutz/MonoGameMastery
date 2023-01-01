using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.VerticalShooter.Particles;

namespace MonoGameMastery.VerticalShooter.Objects
{
    public class MissileSprite : BaseGameObject
    {
        private const float StartSpeed = 0.5f;
        private const float Acceleration = 0.15f;

        private float _speed = StartSpeed;
        private int _missileWidth;
        private int _missileHeight;
        private ExhaustEmitter _exhaustEmitter;

        public override Vector2 Position
        {
            set
            {
                _position = value;
                _exhaustEmitter.Position = new Vector2(_position.X + 16, _position.Y + _missileHeight - 10);
            }
        }

        public MissileSprite(Texture2D missileTexture, Texture2D exhaustTexture) : base(missileTexture)
        {
            _exhaustEmitter = new ExhaustEmitter(exhaustTexture, _position);

            float ratio = (float)_texture2D.Height / (float)_texture2D.Width;
            _missileWidth = 50;
            _missileHeight = (int)(_missileWidth * ratio);
        }

        public override void Update(GameTime gameTime)
        {
            _exhaustEmitter.Update(gameTime);

            Position = new Vector2(Position.X, Position.Y - _speed);
            _speed += Acceleration;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destRect = new Rectangle(Position.ToPoint(), new Point(_missileWidth, _missileHeight));

            spriteBatch.Draw(_texture2D, destRect, Color.White);
            _exhaustEmitter.Draw(spriteBatch);
        }
    }
}