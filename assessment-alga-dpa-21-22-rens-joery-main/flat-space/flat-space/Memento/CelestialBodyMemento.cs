using System;
using System.Collections.Generic;
using System.Text;

namespace flat_space.Memento
{
    public class CelestialBodyMemento
    {
        private List<celestialbody> galaxy { get; set; }

        CelestialBodyMemento(List<celestialbody> galaxy)
        {
            this.galaxy = galaxy;
        }
    }
}
