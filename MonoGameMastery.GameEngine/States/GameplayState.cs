using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Input.Base;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States.Base;

namespace MonoGameMastery.GameEngine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PlayerFighter = "fighter";
        private const string Background = "Barren";

        private const int _viewportWidth = 1280;
        private const int _viewportHeight = 720;


        private TerrainBackground _terrainBackground;
        private PlayerSprite _playerSprite;

        public override void LoadContent()
        {
            _terrainBackground = new TerrainBackground(LoadTexture(Background));
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter));

            AddGameObject(_terrainBackground);
            AddGameObject(_playerSprite);

            _playerSprite.Position = new Vector2(_viewportWidth / 2 - _playerSprite.Width / 2, _viewportHeight / 2 - _playerSprite.Height / 2 - 30);

        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GamePlayInputCommand.GameExit)
                    NotifyEvent(Events.QUIT_GAME);
                if (cmd is GamePlayInputCommand.PlayerMoveLeft)
                {
                    _playerSprite.MoveLeft();
                    KeepPlayerInBounds();
                }
                if (cmd is GamePlayInputCommand.PlayerMoveRight)
                {
                    _playerSprite.MoveRight();
                    KeepPlayerInBounds();
                }
                // if(cmd is GamePlayInputCommand.PlayerShoots)
                //     _playerSprite.Shoot();
            });
        }
        protected override void SetInputManager() => InputManager = new InputManager(new GameplayInputMapper());

        private void KeepPlayerInBounds()
        {
            if (_playerSprite.Position.X < 0)
                _playerSprite.Position = new Vector2(0, _playerSprite.Position.Y);
            if (_playerSprite.Position.X + _playerSprite.Width > _viewportWidth)
                _playerSprite.Position = new Vector2(_viewportWidth - _playerSprite.Width, _playerSprite.Position.Y);
            if (_playerSprite.Position.Y < 0)
                _playerSprite.Position = new Vector2(_playerSprite.Position.X, 0);
            if (_playerSprite.Position.Y < 0)
                _playerSprite.Position = new Vector2(_playerSprite.Position.X, _viewportWidth - _playerSprite.Position.Y);
        }

    }
}