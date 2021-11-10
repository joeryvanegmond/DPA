using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space.Memento
{
    public class CareTaker
    {
        private LinkedList<List<celestialbody>> _mementoList;
        private LinkedListNode<List<celestialbody>> _currentGalaxy;

        public CareTaker() 
        {
            
            _mementoList = new LinkedList<List<celestialbody>>();
        }

        internal void Save(List<celestialbody> galaxy)
        {
            List<celestialbody> celestialbodies = new List<celestialbody>();
            for (int i = 0; i < galaxy.Count; i++)
            {
                if (galaxy[i].GetType() == typeof(Planet))
                {
                    Planet p = (Planet)galaxy[i];
                    celestialbodies.Add(p.Clone());
                }
                else
                {
                    Astroid p = (Astroid)galaxy[i];
                    celestialbodies.Add(p.Clone());
                }
            }

            if (_currentGalaxy == null)
            {
                _mementoList.AddFirst(celestialbodies);
                _currentGalaxy = _mementoList.First;
            }
            else
            {
                _currentGalaxy = _currentGalaxy.ReplaceNext(celestialbodies);
            }
        }

        internal List<celestialbody> Back()
        {
            var counter = 100;

            if (!(_currentGalaxy != null && _currentGalaxy.Previous != null))
            {
                return _currentGalaxy.Value;
            }
            else
            {
                if (_currentGalaxy != null)
                {
                    while (counter > 1)
                    {
                        if (_currentGalaxy.Previous != null)
                        {
                            _currentGalaxy = _currentGalaxy.Previous;
                            counter--;
                        } else
                        {
                            break;
                        }
                    }
                }
                //_currentGalaxy = _currentGalaxy.Previous;
                return _currentGalaxy.Value;
            }
    }

        internal List<celestialbody> Forward()
        {
            var counter = 100;
            if (!(_currentGalaxy != null && _currentGalaxy.Next != null))
            {
                return _currentGalaxy.Value;
            }
            else
            {
                if (_currentGalaxy != null)
                {
                    while (counter > 1)
                    {
                        if (_currentGalaxy.Next != null)
                        {
                            _currentGalaxy = _currentGalaxy.Next;
                            counter--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return _currentGalaxy.Value;
            }
        }
    }
}
