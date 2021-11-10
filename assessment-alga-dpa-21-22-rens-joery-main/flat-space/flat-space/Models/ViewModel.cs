using flat_space.Memento;
using flat_space.QuadTree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace flat_space
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static readonly object ItemsLock = new object();
        private delegate void Update_galaxy_background_callback(celestialbody celestialbody, List<celestialbody> galaxy);
        public ObservableCollection<celestialbody> celestialObjects { get; set; }
        public CareTaker CareTaker { get; }
        public ObservableCollection<fold> folds { get; set; }
        private Planet _largePlanet1 = new Planet();
        private Planet _largePlanet2 = new Planet();
        private Boolean _bfsToggle = false;
        private Boolean _quadShow = false;
        private Boolean _quadNative = false;
        private BreadthFirstSearchClass _bfsClass;
        private Boolean _DijkstraToggle = false;
        private DijkstraClass _DijkstraClass;


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Rectangle> rectangles { get; set; }
        private Tree _tree;


        public ViewModel(List<celestialbody> galaxy)
        {
            celestialObjects = new ObservableCollection<celestialbody>(galaxy);
            this.CareTaker = new CareTaker();
            CareTaker.Save(new List<celestialbody>(celestialObjects));
            BindingOperations.EnableCollectionSynchronization(celestialObjects, ItemsLock);
            folds = new ObservableCollection<fold>();
            createFolds();

            rectangles = new ObservableCollection<Rectangle>();
            BindingOperations.EnableCollectionSynchronization(rectangles, ItemsLock);
            updateQuadTree();
            _bfsClass = new BreadthFirstSearchClass(_largePlanet1, _largePlanet2);

        }

        public void createFolds()
        {
            List<Planet> planets = new List<Planet>();
            for (int i = 0; i < celestialObjects.Count; i++)
            {
                if (celestialObjects[i].GetType() == typeof(Planet))
                {
                    Planet p = (Planet)celestialObjects[i];
                    if (p.neighbours_string != null)
                    {
                        planets.Add(p);
                    }
                }
            }

            _largePlanet1 = new Planet();
            _largePlanet2 = new Planet();

            foreach (var planet in planets)
            {
                //check for largest planets
                if (_largePlanet1.radius < planet.radius)
                {
                    _largePlanet1 = planet;
                }
                else if (_largePlanet2.radius < planet.radius)
                {
                    _largePlanet2 = planet;
                }

                //add folds
                foreach (var planet2 in planet.neighbours_string)
                {
                    string name1 = planet.name;
                    string name2 = planet2;
                    bool newFold = true;
                    foreach (var fold in folds)
                    {
                        if (fold.exists(name1, name2))
                        {
                            newFold = false;
                            break;
                        }
                    }
                    if (newFold)
                    {
                        foreach (var planet2obj in planets)
                        {
                            if (planet2obj.name == planet2)
                            {
                                folds.Add(new fold(planet, planet2obj));
                                planet.addNeighbour(planet2obj);
                                planet2obj.addNeighbour(planet);
                                break;
                            }
                        }
                    }

                }
            }
            rectangles = new ObservableCollection<Rectangle>();
            BindingOperations.EnableCollectionSynchronization(rectangles, ItemsLock);
            updateQuadTree();
            _bfsClass = new BreadthFirstSearchClass(_largePlanet1, _largePlanet2);
            _DijkstraClass = new DijkstraClass(_largePlanet1, _largePlanet2);
        }

        internal void activateQuadNative()
        {
            _quadNative = true;
        }

        internal void activateQuad()
        {
            _quadNative = false;
        }

        internal void activateRectangles()
        {
            if (_quadShow)
            {
                _quadShow = false;
            }
            else
            {
                _quadShow = true;
            }
        }

        public void updateModels()
        {
            var temp = new List<celestialbody>();
            for (int i = 0; i < celestialObjects.Count; i++)
            {
                celestialObjects[i].Move();
            }

            CareTaker.Save(new List<celestialbody>(celestialObjects));

            foreach (var fold in folds)
            {
                fold.update();
            }
            updateQuadTree();
            collistionDetection();
            if (_bfsToggle)
            {
                updateBfs();
            }
            if (_DijkstraToggle)
            {
                updateDijkstra();
            }
        }

        private void collistionDetection()
        {
            bool hasCollision;
            for (int i = 0; i < celestialObjects.Count; i++)
            {
                celestialbody p = celestialObjects[i];
                var range = new Circle(p.x, p.y, p.radius * 2);
                var others = _tree.query(range, null);
                if (!_quadNative)
                {
                    hasCollision = p.CheckCollision(others);
                }
                else
                {
                    hasCollision = p.CheckCollision(new List<celestialbody>(celestialObjects));
                }

                if (hasCollision)
                {
                    p.viewModel = this;
                    switch (p.collision)
                    {
                        case "blink":
                            p.SetCollisionState(new Blink(p));
                            break;
                        case "bounce":
                            p.SetCollisionState(new Bounce(p));
                            break;
                        case "disappear":
                            p.SetCollisionState(new Disappear(p));
                            break;
                        case "explode":
                            p.SetCollisionState(new Explode(p));
                            break;
                        case "grow":
                            p.SetCollisionState(new Grow(p));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Remove(celestialbody celestialbody)
        {
            this.celestialObjects.Remove(celestialbody);
        }

        public void AddSpaceObject(celestialbody celestialbody)
        {
            this.celestialObjects.Add(celestialbody);
        }

        private void drawQuatTree()
        {
            rectangles.Clear();
            _tree = new Tree(new Rectangle(0, 0, 800, 600, true));
            
            for (int i = 0; i < celestialObjects.Count; i++)
            {
                    _tree.insert(celestialObjects[i]);
            }

            var temp = _tree.GetRectangles(_tree);
            if (_quadShow)
            {
                foreach (var item in temp)
                {
                    rectangles.Add(item);
                }
            }
        }

        public void updateQuadTree()
        {
            drawQuatTree();
        }
        
        public void updateBfs()
        {
            SearchPath sp = _bfsClass.Search();
            if(sp != null)
            {
                foreach(Planet p in sp.path)
                {
                    p.visited = true;
                }
                foreach(fold f in folds)
                {
                    f.checkVisited();
                }
            }
        }

        public void disableRoutes()
        {
            foreach (celestialbody p in celestialObjects)
            {
                p.visited = false;
            }
            foreach (fold f in folds)
            {
                f.checkVisited();
            }
        }

        public void bfs()
        {
            if (_bfsToggle == false)
            {
                disableRoutes(); 
                _bfsToggle = true;
                _DijkstraToggle = false;
                updateBfs();
            }
                
        }

        public void dijkstra()
        {
            if (_DijkstraToggle == false)
            {
                disableRoutes();
                _DijkstraToggle = true;
                _bfsToggle = false;
                updateDijkstra();
            }
        }

        public void disable()
        {
            disableRoutes();
            _DijkstraToggle = false;
            _bfsToggle = false;
        }

        public void updateDijkstra()
        {
            disableRoutes();
            Node path = _DijkstraClass.searchRoute();
            if (path != null)
            {
                foreach (fold f in folds)
                {
                    f.checkVisited();
                }
            }
        }

        internal void Restore(List<celestialbody> galaxy)
        {
            this.celestialObjects.Clear();
            foreach (var item in galaxy)
            {
                this.celestialObjects.Add(item);
            }

            rectangles.Clear();
            _tree = new Tree(new Rectangle(0, 0, 800, 600, true));

            for (int i = 0; i < celestialObjects.Count; i++)
            {
                _tree.insert(celestialObjects[i]);
            }

            var temp = _tree.GetRectangles(_tree);
            if (_quadShow)
            {
                foreach (var item in temp)
                {
                    rectangles.Add(item);
                }
            }

            folds.Clear();
            createFolds();
            foreach (fold f in folds)
            {
                f.checkVisited();
            }

        }

        private ICommand _BreathFirstSearch;
        public ICommand BreathFirstSearch
        {
            get
            {
                return _BreathFirstSearch
                    ?? (_BreathFirstSearch = new ActionCommand(() => {
                        bfs();
                    }));
            }
        }

        private Boolean _paused = false;
        public Boolean paused
        {
            get
            {
                return _paused;
            }

            set
            {
                _paused = value;
                OnPropertyChanged("paused");
            }
        }

        private ICommand _PlayPause;
        public ICommand PlayPause
        {
            get
            {
                return _PlayPause
                    ?? (_PlayPause = new ActionCommand(() => {
                        if (paused)
                        {
                            paused = false;
                        }
                        else
                        {
                            paused = true;
                        }
                    }));
            }
        }

        private ICommand _Faster;
        public ICommand Faster
        {
            get
            {
                return _Faster
                    ?? (_Faster = new ActionCommand(() => {
                        Speedup();
                    }));
            }
        }


        private ICommand _Slower;
        public ICommand Slower
        {
            get
            {
                return _Slower
                    ?? (_Slower = new ActionCommand(() => {
                        SlowDown();
                    }));
            }
        }

        private ICommand _Rewind;
        public ICommand Rewind
        {
            get
            {
                return _Rewind
                    ?? (_Rewind = new ActionCommand(() => {
                        Restore(CareTaker.Back());

                    }));
            }
        }

        private ICommand _Quad;
        public ICommand Quad
        {
            get
            {
                return _Quad
                    ?? (_Quad = new ActionCommand(() => {
                        activateQuad();

                    }));
            }
        }

        private ICommand _Native;
        public ICommand Native
        {
            get
            {
                return _Native
                    ?? (_Native = new ActionCommand(() => {
                        activateQuadNative();

                    }));
            }
        }


        public int speed = 50;
        public void Speedup()
        {
            if (speed > 1)
            {
                speed -= 10;
                if (speed < 10)
                {
                    speed = 1;
                }
            }
        }
        public void SlowDown()
        {
            if (speed < 200)
            {
                speed += 10;
            }
        }
    }
}
