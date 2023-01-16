using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.States;
using MonoGameMastery.GameEngine.Util;

namespace MonoGameMastery.GameEngine.Objects;

public abstract class BaseGameObject : BaseObject
{
    private Texture2D _boundingTexture;
    protected Texture2D _texture2D;
    protected List<BoundingBox> _boundingBoxes = new();

    public virtual int Width { get => _texture2D.Width; }
    public virtual int Height { get => _texture2D.Height; }

    public override Vector2 Position
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

    public BaseGameObject(Texture2D texture2D) => _texture2D = texture2D;

    public void AddBoundingBox(BoundingBox bb) => _boundingBoxes.Add(bb);
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Global.DEBUG) DrawBoundingBoxes(spriteBatch);
        spriteBatch.Draw(_texture2D, _position, Color.White);
    }
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
}