using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.VerticalShooter.Particles;

namespace MonoGameMastery.VerticalShooter.Objects
{
    public class MissileSprite : BaseGameObject, IGameObjectWithDamage
    {
        private const float StartSpeed = 0.5f;
        private const float Acceleration = 0.15f;

        private float _speed = StartSpeed;
        private readonly int _missileWidth;
        private readonly int _missileHeight;
        private readonly ExhaustEmitter _exhaustEmitter;

        public override Vector2 Position
        {
            set
            {
                _position = value;
                _exhaustEmitter.Position = new Vector2(_position.X + 16, _position.Y + _missileHeight - 10);
            }
        }

        public int Damage => 25;

        public MissileSprite(Texture2D missileTexture, Texture2D exhaustTexture) : base(missileTexture)
        {
            _exhaustEmitter = new ExhaustEmitter(exhaustTexture, _position);

            float ratio = (float)_texture2D.Height / (float)_texture2D.Width;
            _missileWidth = 50;
            _missileHeight = (int)(_missileWidth * ratio);

            float bbRation = _missileWidth / _texture2D.Width;
            var bbRect = new Rectangle((int)(352 * bbRation), (int)(7 * bbRation), (int)(150 * bbRation), (int)(500 * bbRation));
            AddBoundingBox(new MonoGameMastery.GameEngine.Objects.BoundingBox(bbRect));
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