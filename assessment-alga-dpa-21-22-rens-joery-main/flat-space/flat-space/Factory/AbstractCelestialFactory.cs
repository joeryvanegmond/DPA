using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace flat_space
{
    public interface AbstractCelestialFactory
    {
        public celestialbody CreateObject(XmlNode node);
        public celestialbody CreateObject(string obj);
    }
}