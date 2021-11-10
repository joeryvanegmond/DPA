using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace flat_space
{
    public class Planet : celestialbody
    {
        

        public Planet(string[] data, string name, int x, int y, int radius, float vx, float vy, string color, string oncollision, string[] neighbours)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.vx = vx;
            this.vy = vy;
            this.color = color;
            this.neighbours_string = neighbours;
            this.border = "white";
            this.collision = oncollision;
            this.oldColor = color;
        }

        public Planet()//bouwt lege planet voor vergelijking
        {
            this.name = "";
            this.x = 0;
            this.y = 0;
            this.radius = 0;
            this.vx = 0;
            this.vy = 0;
            this.color = "";
            this.neighbours_string = null;
            this.border = "white";
        }

        private String _name;
        public String name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string[] neighbours_string;
        public Planet[] neighbours;

        public void addNeighbour(Planet nPlanet)
        {
            if(neighbours == null)
            {
                neighbours = new Planet[neighbours_string.Length];
            }
            for (int i = 0; i < neighbours.Length; i++)
            {
                if(neighbours[i] == null)
                {
                    neighbours[i] = nPlanet;
                    break;
                }
            }
        }

        public celestialbody Clone()
        {
            var temp = new Planet();
            temp.name = name;
            temp.x = x;
            temp.y = y;
            temp.radius = radius;
            temp.vx = vx;
            temp.vy = vy;
            temp.color = color;
            temp.neighbours_string = neighbours_string;
            temp.border = "white";
            temp.collision = collision;
            temp.oldColor = color;
            temp.visited = visited;

            return temp;
        }
    }
}