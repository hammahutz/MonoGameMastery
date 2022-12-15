using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects.Base;

public abstract class BaseGameObject
{
    public int ZIndex = 0;

    internal abstract void Render(SpriteBatch spriteBatch);
}