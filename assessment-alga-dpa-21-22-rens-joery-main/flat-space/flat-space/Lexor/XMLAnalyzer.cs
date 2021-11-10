using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace flat_space.Lexor
{
    class XMLAnalyzer : ILexicalAnalyzer
    {


        public override ViewModel ParseFromFile(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return new ViewModel(generateGalaxy(doc));
        }

        public ViewModel ParseFromString(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return new ViewModel(generateGalaxy(doc));
        }

        private List<celestialbody> generateGalaxy(XmlDocument doc)
        {
            List<celestialbody> galaxy = new List<celestialbody>();
            ConcreteCelestialFactory f = new ConcreteCelestialFactory();
            foreach (XmlNode node in doc.DocumentElement)
            {
                galaxy.Add(f.CreateObject(node));
            }
            return galaxy;
        }
    }
}
