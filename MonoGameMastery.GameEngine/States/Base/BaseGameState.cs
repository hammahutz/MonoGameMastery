using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameMastery.GameEngine.Objects.Base;


namespace MonoGameMastery.GameEngine.States.Base;
public abstract class BaseGameState
{
    private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

    public abstract void LoadContent(ContentManager contentManager);
    public abstract void UnloadContent(ContentManager contentManager);
    public abstract void HandleInput();

    public event EventHandler<BaseGameState> OnStateSwitched;

    protected void SwitchState(BaseGameState gameState) => OnStateSwitched?.Invoke(this, gameState);
    protected void AddGameObject(BaseGameObject gameObject) => _gameObjects.Add(gameObject);

    public void Render(SpriteBatch spriteBatch) => _gameObjects.OrderBy(a => a.ZIndex).ToList().ForEach(x => x.Render(spriteBatch));
}
