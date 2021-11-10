using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace flat_space
{
    public class ConcreteCelestialFactory : AbstractCelestialFactory
    {

        public celestialbody CreateObject(XmlNode node)
        {
            int x = int.Parse(node["position"].ChildNodes[0].InnerText);
            int y = int.Parse(node["position"].ChildNodes[1].InnerText);
            int radius = int.Parse(node["position"].ChildNodes[2].InnerText);
            float vx = float.Parse(node["speed"].ChildNodes[0].InnerText);
            float vy = float.Parse(node["speed"].ChildNodes[1].InnerText);
            string color = node["color"].InnerText;
            string oncollision = node["oncollision"].InnerText;
            
            if (node.Name == "planet")
            {
                string name = node["name"].InnerText;
                XmlNodeList neighbours = node["neighbours"].ChildNodes;
                List<string> neighbourslist= new List<string>();
                foreach (XmlNode item in neighbours)
                {
                    neighbourslist.Add(item.InnerText);
                }
                return new Planet(null, name, x, y, radius, vx, vy, color, oncollision, neighbourslist.ToArray());
            }
            else
            {
                return new Astroid(x, y, radius, vx, vy, color, oncollision);
            }
        }

        public celestialbody CreateObject(string obj)
        {
            string[] split = obj.Split(';');
            int x = int.Parse(split[2]);
            int y = int.Parse(split[3]);
            int radius = int.Parse(split[7]);
            float vx = float.Parse(split[4]);
            float vy = float.Parse(split[5]);
            string color = split[8];
            string oncollision = split[9];
            string neighbours = split[6];
            if (split[1] == "Planet")
            {
                string[] neighbourslist = null;
                if (neighbours != null)
                {
                    neighbourslist = neighbours.Split(',');
                }
                return new Planet(null, split[0], x, y, radius, vx, vy, color, oncollision, neighbourslist);
            }
            else
            {
                return new Astroid(x, y, radius, vx, vy, color, oncollision);
            }
        }


    }
}