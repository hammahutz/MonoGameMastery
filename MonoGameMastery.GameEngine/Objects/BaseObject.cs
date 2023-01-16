using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.GameEngine.Objects;

public abstract class BaseObject
{
    protected Vector2 _position;

    public virtual Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public int ZIndex;

    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void OnNotify(BaseGameStateEvent eventType) { }
    public EventHandler<BaseGameStateEvent> OnObjectChanged;
    public void SendEvent(BaseGameStateEvent eventType) => OnObjectChanged?.Invoke(this, eventType);
    public virtual void Destroy() { }
}