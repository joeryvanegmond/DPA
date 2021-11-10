using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class BreadthFirstSearchClass
    {
        private Planet _start;
        private Planet _end;

        public BreadthFirstSearchClass(Planet start, Planet end)
        {
            _start = start;
            _end = end;
        }

        public SearchPath Search()
        {
            Queue<SearchPath> searchPaths = new Queue<SearchPath>();//het totale gelopen path
            Queue<Planet> planetsQueue = new Queue<Planet>();//losse planeten worden nog los bijgehouden, zodat dit makkelijker te checken is met .Contains
            HashSet<Planet> visited = new HashSet<Planet>();//de planeten die al bezocht zijn

            searchPaths.Enqueue(new SearchPath(null, _start));
            planetsQueue.Enqueue(_start);

            while(planetsQueue.Count > 0)
            {
                SearchPath currentPath = searchPaths.Dequeue();
                Planet currentPlanet = planetsQueue.Dequeue();
                visited.Add(currentPlanet);
                foreach (Planet p in currentPlanet.neighbours)
                {
                    if (!visited.Contains(p) && !planetsQueue.Contains(p))
                    {
                        SearchPath newPath = new SearchPath(currentPath, p);
                        if(p == _end)
                        {
                            return newPath;
                        }
                        searchPaths.Enqueue(newPath);
                        planetsQueue.Enqueue(p);
                    }
                }
            }
            return null;
        }

    }
    public class SearchPath
    {
        public List<Planet> path;
        public SearchPath(SearchPath p, Planet target)
        {
            if (p == null) {
                path = new List<Planet>();
            }
            else
            {
                path = new List<Planet>(p.path);
            }           
            path.Add(target);
        }
        public Planet getPlanet()
        {
            return path[path.Count - 1];
        }
    }
}


