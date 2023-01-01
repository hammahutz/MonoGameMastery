using System.Runtime.CompilerServices;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.Util;

namespace MonoGameMastery.VerticalShooter.Objects;

public abstract class BaseChopperPart
{
    protected Rectangle _sourceRectangle;
    protected Rectangle _destinationRectangle;
    protected Vector2 _origin;
    protected Color _color = Color.White;
    protected float _rotation = 0;

    public BaseChopperPart(Rectangle sourceRectangle, Vector2 origin)
    {
        _sourceRectangle = sourceRectangle;
        _origin = origin;
    }

    public void Update(Vector2 position)
    {
        Point pPosition = position.ToPoint();
        Point pOrigin = _sourceRectangle.Origin().ToPoint();
        Point destinationPosition = pPosition + pOrigin;

        _destinationRectangle = new Rectangle(position.ToPoint(), _sourceRectangle.Size);
        UpdatePart(position);
    }

    protected virtual void UpdatePart(Vector2 position) { }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture2D) => spriteBatch.Draw(texture2D, _destinationRectangle, _sourceRectangle, _color, _rotation, _origin, SpriteEffects.None, 0.0f);

}