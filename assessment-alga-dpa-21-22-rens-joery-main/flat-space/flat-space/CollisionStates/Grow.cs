using flat_space.CollisionStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class Grow : BaseCollision
    {
        public Grow(IOnCollisionState context) : base(context)
        {

        }

        public override void OnCollision(celestialbody celestialbody)
        {
            celestialbody.Grow(1);
            if (celestialbody.radius >= 20)
            {
                celestialbody.collision = "explode";
                return;
            }
            
        }
    }
}