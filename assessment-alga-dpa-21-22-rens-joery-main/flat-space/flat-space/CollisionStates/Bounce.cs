using flat_space.CollisionStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class Bounce : BaseCollision
    {
        private int bounced = 0;

        public Bounce(IOnCollisionState context): base(context)
        {
        }

        public override void OnCollision(celestialbody celestialbody)
        {
            if (bounced < 5)
            {
                celestialbody.vx = celestialbody.vx * -1;
                celestialbody.vy = celestialbody.vy * -1;
                return;
            }

            celestialbody.collision = "blink";
        }
    }
}