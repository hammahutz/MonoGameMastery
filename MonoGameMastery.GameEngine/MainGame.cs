using System;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.GameEngine;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static SpriteFont Font;
    private BaseGameState _currentGameState;

    //Book page 65
    private RenderTarget2D _renderTarget;
    private Rectangle _renderScaleRectangle;

    private int _designedResolutionWidth;
    private int _designedResolutionHeight;
    private float _designedResolutionAspectRatio;
    // private float _designedResolutionAspectRation = _designedResolutionWidth / (float)_designedResolutionHeight;

    private BaseGameState _firstGameState;

    public MainGame(int width, int height, BaseGameState firstGameState)
    {
        Content.RootDirectory = "Content";
        _graphics = new GraphicsDeviceManager(this);

        _firstGameState = firstGameState;

        _designedResolutionWidth = width;
        _designedResolutionHeight = height;
        _designedResolutionAspectRatio = width / (float)height;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _designedResolutionWidth;
        _graphics.PreferredBackBufferHeight = _designedResolutionHeight;
        _graphics.IsFullScreen = false;
        IsMouseVisible = true;
        _graphics.ApplyChanges();

        _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, _designedResolutionWidth, _designedResolutionHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        _renderScaleRectangle = GetScaleRectangle();
        base.Initialize();
    }

    private Rectangle GetScaleRectangle()
    {
        var variance = 0.5;
        var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

        Rectangle scaleRectangle;

        if (actualAspectRatio <= _designedResolutionAspectRatio)
        {
            var presentHeight = (int)(Window.ClientBounds.Width / _designedResolutionAspectRatio + variance);
            var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

            scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
        }
        else
        {
            var presentWidth = (int)(Window.ClientBounds.Height * _designedResolutionAspectRatio + variance);
            var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

            scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
        }

        return scaleRectangle;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Font = Content.Load<SpriteFont>("font/Font");

        SwitchGameState(_firstGameState);
    }

    protected override void Update(GameTime gameTime)
    {
        _currentGameState.HandleInput(gameTime);
        _currentGameState.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        DrawGame();
        DrawToScreen();

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        _currentGameState?.UnloadContent();
        base.UnloadContent();
    }


    private void DrawGame()
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);

        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _currentGameState.Draw(_spriteBatch);
        _spriteBatch.End();
    }
    private void DrawToScreen()
    {
        _graphics.GraphicsDevice.SetRenderTarget(null);
        _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);
        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
        _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
        _spriteBatch.End();
    }

    private void SwitchGameState(BaseGameState gameState)
    {
        if (_currentGameState != null)
        {
            _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
            _currentGameState?.UnloadContent();
        }

        _currentGameState = gameState;
        _currentGameState.Initialize(Content, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _currentGameState.LoadContent();

        _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
        _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
    }
    private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
    {
        SwitchGameState(e);
    }

    private void _currentGameState_OnEventNotification(object sender, BaseGameStateEvent e)
    {
        if (e.GetType() == typeof(BaseGameStateEvent.GameQuit))
            Exit();

        // switch (e.GetType())
        // {
        //     case typeof(BaseGameStateEvent.GameQuit:
        //         Exit();
        //         break;
        // }
    }
}
