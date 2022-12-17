using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects.Base;


namespace MonoGameMastery.GameEngine.States.Base;
public abstract class BaseGameState
{
    private List<string> _assets = new List<string>();
    private ContentManager _contentManager;

    public const string FallbackTexture = "Empty";


    private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

    public void Initialize(ContentManager contentManager)
    {
        _contentManager = contentManager;
    }
    public abstract void LoadContent();
    public void UnloadContent()
    {
        _contentManager.Unload();
        //TODO Implement "UnloadAssets" to unload assets just in the game state, perhaps list<sting> assets unloadassets(assets)
        // _contentManager.UnloadAssets(_assets);
        _assets = new List<string>();
    }

    protected Texture2D LoadTexture(string textureName)
    {
        Texture2D texture = _contentManager.Load<Texture2D>(textureName);
        if (texture != null)
        {
            _assets.Add(textureName);
        }
        return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
    }
    public abstract void HandleInput();
    public virtual void Draw(SpriteBatch spriteBatch) => _gameObjects.OrderBy(a => a.ZIndex).ToList().ForEach(x => x.Draw(spriteBatch));

    public event EventHandler<BaseGameState> OnStateSwitched;
    public event EventHandler<Events> OnEventNotification;

    protected void SwitchState(BaseGameState gameState) => OnStateSwitched?.Invoke(this, gameState);
    protected void NotifyEvent(Events eventType, object argument = null)
    {
        OnEventNotification?.Invoke(this, eventType);
        _gameObjects.ForEach(x => x.OnNotify(eventType));
    }
    protected void AddGameObject(BaseGameObject gameObject) => _gameObjects.Add(gameObject);
}
