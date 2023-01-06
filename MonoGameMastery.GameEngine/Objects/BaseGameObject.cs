using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.States;
using MonoGameMastery.GameEngine.Util;

namespace MonoGameMastery.GameEngine.Objects;

public abstract class BaseGameObject
{
    private Texture2D _boundingTexture;
    protected Texture2D _texture2D;
    protected Vector2 _position;
    protected List<BoundingBox> _boundingBoxes = new();


    public int Width { get => _texture2D.Width; }
    public int Height { get => _texture2D.Height; }
    public virtual Vector2 Position
    {
        get => _position;
        set
        {
            var delta = value - _position;

            _boundingBoxes.ForEach(bb => bb.Position += delta);
            _position = value;
        }
    }
    public List<BoundingBox> BoundingBoxes { get => _boundingBoxes; }
    public bool Destroyed { get; set; }

    public int ZIndex = 0;
    public EventHandler<BaseGameStateEvent> OnObjectChanged;

    public BaseGameObject(Texture2D texture2D) => _texture2D = texture2D;

    public virtual void OnNotify(BaseGameStateEvent eventType) { }
    public void SendEvent(BaseGameStateEvent eventType) => OnObjectChanged?.Invoke(this, eventType);
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (Global.DEBUG) DrawBoundingBoxes(spriteBatch);
        spriteBatch.Draw(_texture2D, _position, Color.White);
    }

    public void AddBoundingBox(BoundingBox bb) => _boundingBoxes.Add(bb);
    public void DrawBoundingBoxes(SpriteBatch spriteBatch)
    {
        if (_boundingTexture == null)
        {
            CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
        }
        _boundingBoxes.ForEach(bb => spriteBatch.Draw(_boundingTexture, bb.Rectangle, Color.Red));
    }

    private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
    {
        _boundingTexture = new Texture2D(graphicsDevice, 1, 1);
        _boundingTexture.SetData<Color>(new Color[] { Color.White });
    }

    public virtual void Destroy()
    {

    }
}