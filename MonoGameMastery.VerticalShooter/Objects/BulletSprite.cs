using System.Reflection.Metadata;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects;

public class BulletSprite : BaseGameObject, IGameObjectWithDamage
{
    private const float BULLET_SPEED = 10.0f;
    private Rectangle BB = new Rectangle(9, 4, 10, 22);
    public BulletSprite(Texture2D texture2D) : base(texture2D) 
    {
        AddBoundingBox(new MonoGameMastery.GameEngine.Objects.BoundingBox(BB));
    }

    public int Damage => 10;

    public override void Update(GameTime gameTime) => Position = new Vector2(Position.X, Position.Y - BULLET_SPEED);
}