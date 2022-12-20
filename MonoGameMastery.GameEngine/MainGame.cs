using System.Security.Cryptography.X509Certificates;
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.States;
using MonoGameMastery.GameEngine.States.Base;

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

    private const int DESIGNED_RESOLUTION_WIDTH = 1280;
    private const int DESIGNED_RESOLUTION_HEIGHT = 720;
    private const float DESIGNED_RESOLUTION_ASPECT_RATION = DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1024;
        _graphics.PreferredBackBufferWidth = 768;
        _graphics.IsFullScreen = false;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        _renderScaleRectangle = GetScaleRectangle();
        base.Initialize();
    }

    private Rectangle GetScaleRectangle()
    {
        double variance = 0.5;
        float actualAspectRation = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

        Rectangle scaleRectangle;


        if (actualAspectRation <= DESIGNED_RESOLUTION_ASPECT_RATION)
        {
            int presentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATION + variance);
            int barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
            scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
        }
        else
        {
            int presentWidth = (int)(Window.ClientBounds.Height / DESIGNED_RESOLUTION_ASPECT_RATION + variance);
            int barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
            scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
        }

        return scaleRectangle;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Font = Content.Load<SpriteFont>("font/Font");

        SwitchGameState(new SplashState());
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
        _currentGameState.OnEventNotification  += _currentGameState_OnEventNotification;
    }
    private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
    {
        SwitchGameState(e);
    }

    private void _currentGameState_OnEventNotification(object sender, Events e)
    {
        switch (e)
        {
            case Events.QUIT_GAME:
                Exit();
                break;
        }
    }
}
