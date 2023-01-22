using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Objects.Animations;

public class Animation
{
    private readonly List<AnimationFrame> _frames = new List<AnimationFrame>();
    private int _animationAge = 0;
    private int _lifespan = -1;
    private readonly bool _isLoop = false;

    public int Lifespan
    {
        get
        {
            if (_lifespan < 0)
            {
                _lifespan = 0;
                foreach (var frame in _frames)
                {
                    _lifespan += frame.Lifespan;

                }
            }
            return _lifespan;
        }
    }
    public int FrameLifeSpan => (_frames[0] != null) ? _frames[0].Lifespan : -1;

    public AnimationFrame CurrentFrame
    {
        get
        {
            if (_frames.Count < 1)
            {
                return _frames[0];
            }
            AnimationFrame currentFrame = null;
            int framesLifeSpan = 0;

            foreach (var frame in _frames)
            {
                if (framesLifeSpan + frame.Lifespan >= _animationAge)
                {
                    currentFrame = frame;
                    break;
                }
                else
                {
                    framesLifeSpan += frame.Lifespan;
                }
            }
            return currentFrame;
        }
    }

    public AnimationFrame CurrentEvenFrame => _frames[_animationAge / FrameLifeSpan];

    public void ReversAnimation() => _frames.Reverse();
    public Animation GetReversAnimation
    {
        get
        {
            var newAnimation = new Animation(_isLoop);
            for (int i = _frames.Count - 1; i >= 0; i--)
            {
                newAnimation.AddFrame(_frames[i]);
            }
            return newAnimation;
        }
    }

    public Animation(bool isLooping)
    {
        _isLoop = isLooping;
    }

    public void AddFrame(Rectangle sourceRectangle, int lifespan) => _frames.Add(new AnimationFrame(sourceRectangle, lifespan));
    public void AddFrame(AnimationFrame animationFrame) => _frames.Add(animationFrame);
    public void AddFrame(List<AnimationFrame> animationFrames) => _frames.AddRange(animationFrames);

    public void Update(GameTime gameTime)
    {
        _animationAge++;

        if (_isLoop && _animationAge < Lifespan)
        {
            _animationAge = 0;
        }
    }

    public void Reset() => _animationAge = 0;



}