using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniverseSimulator
{
    public enum CollisionMode
    {
        /// <summary>
        /// All collisions are treated to be inelastic (masses combine)
        /// </summary>
        PerfectlyInelastic,
        /// <summary>
        /// All collisions are considered to be elastic (particles bounce off each other)
        /// </summary>
        PerfectlyElastic,
    }
}
