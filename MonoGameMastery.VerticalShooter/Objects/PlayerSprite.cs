using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects.Animations;
using MonoGameMastery.GameEngine.Util;

namespace MonoGameMastery.GameEngine.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        private const float PLAYER_SPEED = 10.0f;
        private List<Rectangle> BB;

        private Dictionary<AnimationState, Animation> _animations;
        private Animation _currentAnimation;
        private Rectangle _idleRectangle;
        private bool _movingLeft = false;
        private bool _movingRight = false;
        private Rectangle _destinationRectangle;
        private Rectangle? _sourceRectangle;
        private const int ANIMATION_SPEED = 3;
        private const int ANIMATION_CELL_WIDTH = 116;
        private const int ANIMATION_CELL_HEIGHT = 152;
        private const int SPRITE_SHEET_X = 0;
        private const int SPRITE_SHEET_Y = 0;
        private const int SPRITE_SHEET_COLUMNS = 7;
        private const int SPRITE_SHEET_ROWS = 1;

        public override int Width => ANIMATION_CELL_WIDTH;
        public override int Height => ANIMATION_CELL_HEIGHT;


        public PlayerSprite(Texture2D texture2D) : base(texture2D)
        {
            BB = new List<Rectangle>()
            {
                new Rectangle(29,2,57,147),
                new Rectangle(2,77,111,37),
            };

            BB.ForEach(bb => AddBoundingBox(new BoundingBox(bb)));

            _animations = SetUpAnimation();
            _currentAnimation = _animations[AnimationState.Idle];

        }

        private static Dictionary<AnimationState, Animation> SetUpAnimation()
        {
            var animations = new Dictionary<AnimationState, Animation>()
            {
                {AnimationState.Idle, new Animation(true)},
                {AnimationState.TurnLeft, new Animation(false)},
                {AnimationState.TurnRight, new Animation(false)},
                {AnimationState.LeftToCenter, new Animation(false)},
                {AnimationState.RightToCenter, new Animation(false)},
            };

            Rectangle[,] spriteSheet = MathUtil.SpriteSheet
            (
                new Rectangle(SPRITE_SHEET_X, SPRITE_SHEET_Y, SPRITE_SHEET_COLUMNS, SPRITE_SHEET_ROWS),
                new Point(ANIMATION_CELL_WIDTH, ANIMATION_CELL_HEIGHT)
            );

            animations[AnimationState.Idle].AddFrame(new List<AnimationFrame>()
            {
                new AnimationFrame(spriteSheet[3,0],ANIMATION_SPEED)
            });
            animations[AnimationState.TurnLeft].AddFrame(new List<AnimationFrame>()
            {
                new AnimationFrame(spriteSheet[3,0] ,ANIMATION_SPEED),
                new AnimationFrame(spriteSheet[2,0] ,ANIMATION_SPEED),
                new AnimationFrame(spriteSheet[1,0] ,ANIMATION_SPEED),
                new AnimationFrame(spriteSheet[0,0] ,ANIMATION_SPEED),
            });

            animations[AnimationState.TurnRight].AddFrame(new List<AnimationFrame>()
            {
                new AnimationFrame(spriteSheet[3,0] ,ANIMATION_SPEED),
                new AnimationFrame(spriteSheet[4,0] ,ANIMATION_SPEED),
                new AnimationFrame(spriteSheet[5,0] ,ANIMATION_SPEED),
                new AnimationFrame(spriteSheet[6,0] ,ANIMATION_SPEED),
            });

            animations[AnimationState.LeftToCenter] = animations[AnimationState.TurnLeft].GetReversAnimation;
            animations[AnimationState.RightToCenter] = animations[AnimationState.TurnRight].GetReversAnimation;

            return animations;
        }

        public void StopMoving()
        {
            if (_movingLeft)
            {
                _currentAnimation = _animations[AnimationState.LeftToCenter];
            }
            if (_movingRight)
            {
                _currentAnimation = _animations[AnimationState.RightToCenter];
            }
            _movingLeft = false;
            _movingRight = false;
        }

        public void MoveLeft()
        {
            _movingLeft = true;
            _movingRight = false;
            _currentAnimation = _animations[AnimationState.TurnLeft];
            _animations[AnimationState.LeftToCenter].Reset();
            _animations[AnimationState.TurnRight].Reset();
            Position = new Vector2(Position.X - PLAYER_SPEED, Position.Y);
        }
        public void MoveRight()
        {
            _movingRight = true;
            _movingLeft = false;
            _currentAnimation = _animations[AnimationState.TurnRight];
            _animations[AnimationState.RightToCenter].Reset();
            _animations[AnimationState.TurnLeft].Reset();
            Position = new Vector2(Position.X + PLAYER_SPEED, Position.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentAnimation != null)
                _currentAnimation.Update(gameTime);

            _destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, ANIMATION_CELL_WIDTH, ANIMATION_CELL_HEIGHT);
            _sourceRectangle = (_currentAnimation.CurrentFrame != null) ? _currentAnimation.CurrentFrame.SourceRectangle : _sourceRectangle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBoundingBoxes(spriteBatch);
            spriteBatch.Draw(_texture2D, _destinationRectangle, _sourceRectangle, Color.White);
        }

        private enum AnimationState
        {
            Idle,
            TurnLeft,
            TurnRight,
            LeftToCenter,
            RightToCenter
        }
    }


}