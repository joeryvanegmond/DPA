using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space.QuadTree
{
    public class Circle
    {
        private float _x;
        public float x
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        private float _y;
        public float y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }

        private float _radius;
        public float radius
        {
            get
            {
                return _radius;
            }

            set
            {
                _radius = value;
            }
        }

        private int _rSquared;
        public int rSquared
        {
            get
            {
                return _rSquared;
            }

            set
            {
                _rSquared = value;
            }
        }

        public Circle(float x, float y, int radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.rSquared = radius * radius;

        }

        public bool contains(celestialbody cell)
        {
            // check if the point is in the circle by checking if the euclidean distance of
            // the point and the center of the circle if smaller or equal to the radius of
            // the circle
            var d = Math.Pow((cell.x - this.x), 2) + Math.Pow((cell.y - this.y), 2);
            return d <= this.rSquared;
        }

        public bool intersects(Rectangle range)
        {

            var xDist = Math.Abs(range.x - this.x);
            var yDist = Math.Abs(range.y - this.y);

            // radius of the circle
            var r = this.radius;

            var w = range.width;
            var h = range.height;

            var edges = Math.Pow((xDist - w), 2) + Math.Pow((yDist - h), 2);

            // no intersection
            if (xDist > (r + w) || yDist > (r + h))
                return false;

            // intersection within the circle
            if (xDist <= w || yDist <= h)
                return true;

            // intersection on the edge of the circle
            return edges <= this.rSquared;
        }

    }
}
