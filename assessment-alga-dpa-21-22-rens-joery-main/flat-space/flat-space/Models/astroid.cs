using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class Astroid : celestialbody
    {

        public Astroid(float x, float y, int radius, float vx, float vy, string color, string oncollision)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.vx = vx;
            this.vy = vy;
            this.color = color;
            this.border = "white";
            this.collision = oncollision;
            this.oldColor = color;
        }

        public celestialbody Clone()
        {
            var temp = new Astroid(x, y, radius, vx, vy, border, collision);
            temp.oldColor = oldColor;
            return temp;
        }
    }
}