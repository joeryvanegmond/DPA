using flat_space.CollisionStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class Explode : BaseCollision
    {
        public Explode(IOnCollisionState context) : base(context)
        {

        }

        public override void OnCollision(celestialbody celestialbody)
        {
            Random rn = new Random();
            celestialbody copy = celestialbody;
            celestialbody.Remove();
            copy.radius = 5;
            copy.collision = "bounce";

            for (int i = 0; i < 5; i++)
            {
                celestialbody.AddSpaceObject(new Astroid((copy.x + (i * 3) + copy.radius), (copy.y + (i * 3) + copy.radius), copy.radius, copy.vx * -1 + rn.Next(5), copy.vy * -1 + rn.Next(8), copy.color, copy.collision));
            }
        }
    }
}