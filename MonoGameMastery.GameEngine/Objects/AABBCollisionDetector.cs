using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameMastery.GameEngine.Objects;

public class AABBCollisionDetector<P, A> where P : BaseGameObject where A : BaseGameObject
{
    private readonly List<P> _passiveObjects = new List<P>();

    private readonly List<string> _namn = new List<string>() { "adam", "sunanda" };

    public AABBCollisionDetector(List<P> passiveObjects)
    {
        _passiveObjects = passiveObjects;

        var mittnamn = _namn.Where(x => x == "adam").ToList();
    }

    public void DetectCollisions(A activeObject, Action<P, A> collisionsHandler) => _passiveObjects
                                                                                    .Where(passiveObject => DetectCollisions(passiveObject, activeObject)).ToList()
                                                                                    .ForEach(passiveObject => collisionsHandler(passiveObject, activeObject));

    public void DetectCollisions(List<A> activeObject, Action<P, A> collisionsHandler) => _passiveObjects
                                                                                           .ForEach(pO => activeObject
                                                                                                .Where(aO => DetectCollisions(pO, aO)).ToList()
                                                                                                .ForEach(aO => collisionsHandler(pO, aO)));

    /*
        public bool DetectCollisions(P passiveObject, A activeObject)
        {
            foreach (var passiveBB in passiveObject.BoundingBoxes)
            {
                foreach (var activeBB in activeObject.BoundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }*/

    public bool DetectCollisions(P passiveObject, A activeObject) => passiveObject.BoundingBoxes
        .Where(x => activeObject.BoundingBoxes
            .Where(y => x.CollidesWith(y))
            .Any())
        .Any();
}