using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace flat_space.Lexor
{
    class CSVAnalyzer : ILexicalAnalyzer
    {
        public override ViewModel ParseFromFile(String path)
        {
            try
            {
                String[] lines = File.ReadAllLines(path);
                return new ViewModel(generateGalaxy(lines));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ViewModel ParseFromString(String csv)
        {
            string[] stringSeparators = new string[] { "\r\n" };

            try
            {
                string[] lines = csv.Split(stringSeparators, StringSplitOptions.None);
                return new ViewModel(generateGalaxy(lines));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<celestialbody> generateGalaxy(String[] lines)
        {
            ConcreteCelestialFactory factory = new ConcreteCelestialFactory();
            List<celestialbody> galaxy = new List<celestialbody>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (i != 0 && !String.IsNullOrEmpty(lines[i]))
                {
                    galaxy.Add(factory.CreateObject(lines[i]));
                }
            }
            return galaxy;
        }
    }
}
