using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Objects.Animations;

public class AnimationFrame
{
    public AnimationFrame(Rectangle sourceRectangle, int lifespan)
    {
        SourceRectangle = sourceRectangle;
        Lifespan = lifespan;
    }

    public Rectangle SourceRectangle { get; private set; }
    public int Lifespan { get; private set; }

}