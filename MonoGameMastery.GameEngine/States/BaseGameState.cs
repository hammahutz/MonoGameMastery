using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.Sound;

namespace MonoGameMastery.GameEngine.States;
public abstract class BaseGameState
{
    private List<string> _assets = new List<string>();
    private ContentManager _contentManager;
    protected SoundManager _soundManager = new SoundManager();
    private int _viewportWidth;
    private int _viewportHeight;
    public const string FallbackTexture = "Empty";

    protected InputManager InputManager { get; set; }


    private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

    public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
    {
        _contentManager = contentManager;
        _viewportWidth = viewportWidth;
        _viewportHeight = viewportHeight;

        SetInputManager();
    }
    public abstract void LoadContent();
    public SoundEffect LoadSounds(string soundName) => _contentManager.Load<SoundEffect>(soundName);
    public void UnloadContent()
    {
        _contentManager.Unload();
        //TODO Implement "UnloadAssets" to unload assets just in the game state, perhaps list<sting> assets unloadassets(assets)
        // _contentManager.UnloadAssets(_assets);
        _assets = new List<string>();
    }

    public abstract void HandleInput(GameTime gameTime);

    public virtual void UpdateGameState(GameTime gameTime){}
    public void Update(GameTime gameTime)
    {
        UpdateGameState(gameTime);
        _soundManager.PlaySoundtrack();
    }

    public virtual void Draw(SpriteBatch spriteBatch) => _gameObjects.OrderBy(a => a.ZIndex).ToList().ForEach(x => x.Draw(spriteBatch));
    protected Texture2D LoadTexture(string textureName)
    {
        Texture2D texture = _contentManager.Load<Texture2D>(textureName);
        if (texture != null)
        {
            _assets.Add(textureName);
        }
        return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
    }

    protected void SwitchState(BaseGameState gameState) => OnStateSwitched?.Invoke(this, gameState);
    protected void NotifyEvent(BaseGameStateEvent eventType, object argument = null)
    {
        OnEventNotification?.Invoke(this, eventType);
        _gameObjects.ForEach(x => x.OnNotify(eventType));

        _soundManager.OnNotify(eventType);
    }
    protected void AddGameObject(BaseGameObject gameObject) => _gameObjects.Add(gameObject);
    protected void RemoveGameObject(BaseGameObject gameObject) => _gameObjects.Remove(gameObject);
    protected abstract void SetInputManager();
    public event EventHandler<BaseGameState> OnStateSwitched;
    public event EventHandler<BaseGameStateEvent> OnEventNotification;


}
