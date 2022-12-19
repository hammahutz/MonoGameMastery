using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.GameEngine.Objects.Base;

public abstract class BaseGameObject
{
    protected Texture2D _texture2D;
    protected Vector2 _position;

    public int Width { get => _texture2D.Width; }
    public int Height { get => _texture2D.Height; }
    public Vector2 Position { get => _position; set => _position = value; }

    public int ZIndex = 0;

    public virtual void OnNotify(Events eventType) { }

    public virtual void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture2D, _position, Color.White);
}