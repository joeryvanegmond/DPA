using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace flat_space.QuadTree
{
    public class Tree
    {
        private List<celestialbody> _cells;
        private static int _capacity = 4;
        public Rectangle boundary;
        public Tree topLeft;
        public Tree topRight;
        public Tree bottomLeft;
        public Tree bottomRight;
        private bool divided = false;
        private List<Rectangle> _rectangles;


        public Tree(Rectangle boundary)
        {
            this.boundary = boundary;
            _cells = new List<celestialbody>();
            _rectangles = new List<Rectangle>();
        }

        private void subdivide()
        {
            int x = boundary.x;
            int y = boundary.y;
            int width = boundary.width;
            int height = boundary.height;


            Rectangle tl = new Rectangle(
                x,
                y,
                (width / 2),
                (height / 2));
            topLeft = new Tree(tl);

            Rectangle tr = new Rectangle(
                (x + width / 2),
                y,
                (width / 2),
                (height / 2));
            topRight = new Tree(tr);

            Rectangle bl = new Rectangle(
                x,
                (y + height / 2),
                (width / 2),
                (height / 2));
            bottomLeft = new Tree(bl);

            Rectangle br = new Rectangle(
                (x + width / 2),
                (y + height / 2),
                (width / 2),
                (height / 2));
            bottomRight = new Tree(br);
            divided = true;
        }



        public void insert(celestialbody cell)
        {
            if (!boundary.contains(cell)) return;

            if (_cells.Count < _capacity && !divided)
            {
                _cells.Add(cell);
                return;
            }
            else
            {
                if (!divided)
                {
                    subdivide();
                    foreach (var item in _cells)
                    {
                        topLeft.insert(item);
                        topRight.insert(item);
                        bottomLeft.insert(item);
                        bottomRight.insert(item);
                    }
                    _cells.Clear();
                }
                topLeft.insert(cell);
                topRight.insert(cell);
                bottomLeft.insert(cell);
                bottomRight.insert(cell);
            }
        }

        public List<celestialbody> query(Circle range, List<celestialbody> found)
        {
            if (found == null)
            {
                found = new List<celestialbody>();
            }

            if (!range.intersects(this.boundary))
            {
                return found;
            }

            foreach (var cell in _cells)
            {
                if (range.contains(cell))
                {
                    found.Add(cell);
                }
            }

            if (divided)
            {
                topLeft.query(range, found);
                topRight.query(range, found);
                bottomLeft.query(range, found);
                bottomRight.query(range, found);
            }
            

            return found;
        }

        public List<Rectangle> GetRectangles(Tree tree)
        {
            _rectangles.Add(tree.boundary);
            if (tree.topLeft != null) 
            {
                _rectangles.Add(tree.topLeft.boundary);
                GetRectangles(tree.topLeft);
            }

            if (tree.topRight != null)
            {
                _rectangles.Add(tree.topRight.boundary);
                GetRectangles(tree.topRight);
            }

            if (tree.bottomLeft != null)
            {
                _rectangles.Add(tree.bottomLeft.boundary);
                GetRectangles(tree.bottomLeft);
            }

            if (tree.bottomRight != null)
            {
                _rectangles.Add(tree.bottomRight.boundary);
                GetRectangles(tree.bottomRight);
            }

            return _rectangles;
        }
    }
}
