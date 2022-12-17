using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.GameEngine.Objects.Base;

public abstract class BaseGameObject
{
    protected Texture2D _texture2D;
    public int ZIndex = 0;

    public virtual void OnNotify(Events eventType) { }

    public virtual void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture2D, Vector2.Zero, Color.White);
}