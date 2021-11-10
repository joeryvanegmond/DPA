using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace flat_space.QuadTree
{
    public class Rectangle : INotifyPropertyChanged
    {
        private int _x;
        public int x
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
                OnPropertyChanged("x");
            }
        }

        private int _y;
        public int y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
                OnPropertyChanged("y");
            }
        }

        private int _width;
        public int width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
                OnPropertyChanged("width");
            }
        }

        private int _height;
        public int height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
                OnPropertyChanged("height");
            }
        }

        private bool _main;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Rectangle(int x, int y, int width, int height, bool main = false)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this._main = main;
        }

        public bool contains(celestialbody cell)
        {
            return (cell.x >= x &&
                cell.x <= x + width &&
                cell.y >= y &&
                cell.y <= y + height);
        }

        public bool intersects(Rectangle range)
        {
            return !(range.x - range.width > x + width ||
                range.x + range.width < x - width ||
                range.y - range.height > y + height ||
                range.y + range.height < y - height);
        }
    }
}
