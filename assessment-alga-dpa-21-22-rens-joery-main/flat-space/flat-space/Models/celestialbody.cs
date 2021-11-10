using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using flat_space.CollisionStates;
using System.Timers;

namespace flat_space
{
    public abstract class celestialbody : INotifyPropertyChanged, IOnCollisionState
    {
        private int _radius;
        public int radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                OnPropertyChanged("radius");
            }
        }

        private string _oldColor;
        public string oldColor
        {
            get
            {
                return _oldColor;
            }

            set
            {
                if (value == "grey")
                {
                    _oldColor = "gray";
                }
                else
                {
                    _oldColor = value;
                }
            }
        }

        private string _color;
        public string color
        {
            get
            {
                return _color;
            }
            set
            {
                if(value == "grey")
                {
                    _color = "gray";
                }
                else
                {
                    _color = value;
                }
                OnPropertyChanged("color");
            }
        }

        private float _vx;
        public float vx
        {
            get
            {
                return _vx;
            }
            set
            {
                _vx = value;
                OnPropertyChanged("vx");
            }
        }

        private float _vy;
        public float vy
        {
            get
            {
                return _vy;
            }
            set
            {         
                _vy = value;
                OnPropertyChanged("vy");
            }
        }

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
                OnPropertyChanged("x");
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
                OnPropertyChanged("y");
            }
        }

        private string _collision;
        public string collision
        {
            get
            {
                return _collision;
            }

            set
            {
                _collision = value;
                OnPropertyChanged("collision");
            }
        }

        private string _border;
        public string border { get { return _border; } set { _border = value; OnPropertyChanged("border"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ViewModel viewModel;

        public void Move()
        {
            x = x + vx;
            y = y + vy;
            CheckBorderCollision();
        }

        private void CheckBorderCollision()
        {
            float width = 800;
            float height = 600;

            if (x - radius <= 0)
            {
                x += (radius - x);
                vx = -vx;
            }
            else if (x + radius > width)
            {
                x += (width - radius - x);
                vx = -vx;
            }

            if (y - radius <= 0)
            {
                y += (radius - y);
                vy = -vy;
            }
            else if (y + radius > height)
            {
                y += (height - radius - y);
                vy = -vy;
            }
        }

        private Boolean _visited = false;
        public Boolean visited
        {
            get
            {
                return _visited;
            }
            set
            {
                _visited = value;
            }
        }

        public bool CheckCollision(List<celestialbody> others)
        {
            foreach (var other in others)
            {
                if (this != other)
                {
                    var distance = Math.Sqrt(Math.Pow((other.x - this.x), 2) + Math.Pow((other.y - this.y), 2));
                    return distance < (this.radius / 2) + (other.radius / 2);
                }
            }
            return false;
        }

        public void ReturnToMoment()
        {
            throw new System.NotImplementedException();
        }

        public void SetCollisionState(BaseCollision state)
        {
            state.OnCollision(this);
        }

        public void Grow(int size)
        {
            this.radius += size;
        }

        public void Remove()
        {
            viewModel.Remove(this);
        }

        public void AddSpaceObject(celestialbody celestialbody)
        {
            viewModel.AddSpaceObject(celestialbody);
        }

        public void SetVelocity(float vx, float vy)
        {
            this.vx = vx;
            this.vy = vy;
        }
    }
}