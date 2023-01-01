using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects;

public class BulletSprite : BaseGameObject
{
    private const float BULLET_SPEED = 10.0f;
    public BulletSprite(Texture2D texture2D) : base(texture2D) { }
    public override void Update(GameTime gameTime) => Position = new Vector2(Position.X, Position.Y - BULLET_SPEED);
}