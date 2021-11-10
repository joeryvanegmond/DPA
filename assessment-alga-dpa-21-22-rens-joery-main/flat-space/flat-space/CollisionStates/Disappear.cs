using flat_space.CollisionStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class Disappear : BaseCollision
    {
        public Disappear(IOnCollisionState context): base(context)
        {

        }

        public override void OnCollision(celestialbody celestialbody)
        {
            celestialbody.Remove();
        }
    }
}