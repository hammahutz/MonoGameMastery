using System;

using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.GameEngine.Objects.Base;

public abstract class BaseGameObject
{
    public int ZIndex = 0;

    public virtual void OnNotify(Events eventType) { }

    internal abstract void Render(SpriteBatch spriteBatch);
}