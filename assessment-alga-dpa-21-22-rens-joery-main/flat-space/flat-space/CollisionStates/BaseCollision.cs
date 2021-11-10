using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space.CollisionStates
{
    public abstract class BaseCollision
    {
        public string collision { get; internal set; }

        protected IOnCollisionState Context;

        public BaseCollision(IOnCollisionState context)
        {
            Context = context;
        }

        public abstract void OnCollision(celestialbody celestialbody);
    }
}
