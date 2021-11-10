using System;
using System.ComponentModel;

namespace flat_space
{
    public class fold : INotifyPropertyChanged
    {
        private float _X1;
        private float _X2;
        private float _Y1;
        private float _Y2;

        public float X1 { get { return _X1; } set { _X1 = value; OnPropertyChanged("X1"); } }
        public float Y1 { get { return _Y1; } set { _Y1 = value; OnPropertyChanged("Y1"); } }
        public float X2 { get { return _X2; } set { _X2 = value; OnPropertyChanged("X2"); } }
        public float Y2 { get { return _Y2; } set { _Y2 = value; OnPropertyChanged("Y2"); } }
        public Planet p1 { get; set; }
        public Planet p2 { get; set; }
        private string _border;
        public string border { get { return _border; } set { _border = value; OnPropertyChanged("border"); } }

        public fold(Planet p1, Planet p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            X1 = p1.x + (p1.radius / 2);
            Y1 = p1.y + (p1.radius / 2);
            X2 = p2.x + (p2.radius / 2);
            Y2 = p2.y + (p2.radius / 2);
            this.border = "white";
        }

        public bool exists(string s1, string s2)
        {
            if ((s1 == p1.name && s2 == p2.name) || (s2 == p1.name && s1 == p2.name))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        internal void update()
        {
            X1 = p1.x + (p1.radius / 2);
            Y1 = p1.y + (p1.radius / 2);
            X2 = p2.x + (p2.radius / 2);
            Y2 = p2.y + (p2.radius / 2);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void checkVisited()
        {
            if(p1.visited && p2.visited)
            {
                border = "red";
            }
            else
            {
                border = "white";
            }
        }
    }
}