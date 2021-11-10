using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space
{
    public class DijkstraClass
    {
        private Planet _start;
        private Planet _end;
        private Node nodes;

        public DijkstraClass(Planet start, Planet end)
        {
            _start = start;
            _end = end;
        }

        public Node searchRoute() 
        {
            HashSet<Planet> nodeCreated = new HashSet<Planet>();
            List<Node> nodes = new List<Node>();
            List<Node> completedNodes = new List<Node>();
            Node current = new Node(_start, 0);
            nodeCreated.Add(_start);
            nodes.Add(current);

            while (current != null)
            {
                if(current.planet == _end)
                {
                    break;
                }
                foreach (var neighbour in current.planet.neighbours)
                {
                    Node NeigbourNode = new Node(null);
                    if (!nodeCreated.Contains(neighbour))//als er nog geen bezoek is geweest aan de planet, maak een nieuwe node aan.
                    {
                        NeigbourNode = new Node(neighbour, -1, current);
                        nodes.Add(NeigbourNode);
                        nodeCreated.Add(neighbour);

                        
                    }
                    else
                    {//als de node al eerder bezocht is, zoek deze
                        foreach (Node n in nodes)
                        {
                            if (n.planet == neighbour)
                            {
                                NeigbourNode = n;
                            }
                        }
                    }
                    int distance = current.shortestRoute + calcDistance(current.planet, neighbour); //bereken distance tussen current en neighbour node, zorg ervoor dat deze wordt opgeslagen als deze kleiner is
                    if(NeigbourNode.shortestRoute == -1 || NeigbourNode.shortestRoute > distance)
                    {
                        NeigbourNode.shortestRoute = distance;
                        NeigbourNode.previous = current;
                    }
                }

                completedNodes.Add(current);//geef door dat deze node compleet is
                nodes.Remove(current);
                if(nodes.Count == 0)
                {
                    current = null;

                }
                else
                {
                    current = nodes[0];//ga door naar volgende node die het korste is
                    foreach (Node n in nodes)
                    {
                        if(n.shortestRoute < current.shortestRoute)
                        {
                            current = n;
                        }
                    }
                }
                
            }

            //maak alle nodes visited
            current.SetAsRoute();
            return current;
            
        }

        public int calcDistance(Planet p1, Planet p2)
        {
            float x = p2.x - p1.x;
            float y = p2.y - p1.y;
            float distance = (float)Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2.0));
            return (int) distance;
        }
    }

    public class Node
    {
        public Planet planet;
        public Node previous;
        public int shortestRoute;

        public Node(Planet p, int value = -1, Node previous = null)
        {
            planet = p;
            shortestRoute = value;
            this.previous = previous;
        }

        public void SetAsRoute()
        {
            planet.visited = true;
            if(previous != null)
            {
                previous.SetAsRoute();
            }
        }
    }
}
