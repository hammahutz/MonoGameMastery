using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects;

public abstract class BaseTextObject : BaseObject
{
    protected SpriteFont _font;
    public string Text { get; set; }
    public BaseTextObject(SpriteFont font) => _font = font;
    public override void Draw(SpriteBatch spriteBatch) => spriteBatch.DrawString(_font, Text, _position, Color.White);
}