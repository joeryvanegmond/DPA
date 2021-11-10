using flat_space.CollisionStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public interface IOnCollisionState
    {
        float x { get; }
        float y { get; }

        float vx { get; }

        float vy { get; }

        int radius { get; }

        string color { get; }


        void SetCollisionState(BaseCollision state);

        void Grow(int size);

        void Remove();

        void AddSpaceObject(celestialbody celestialbody);

        void SetVelocity(float vx, float vy);

    }
}