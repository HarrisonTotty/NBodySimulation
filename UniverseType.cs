using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniverseSimulator
{
    public enum UniverseBoundaryType
    {
        /// <summary>
        /// Universe has no concept of an edge. There is no limit to distance.
        /// </summary>
        Infinate,
        /// <summary>
        /// The universe has a fixed size. The boundary acts like a wall.
        /// </summary>
        Fixed_HardEdge,
        /// <summary>
        /// The universe has a fixed size particles that pass through the edge will emerge on the opposite side.
        /// </summary>
        Fixed_Wrapped,
        /// <summary>
        /// Similar to wrapped except particles experiance the force of the greater varient of every other particle (A particle near the edge will feel forces from beyond the edge)
        /// </summary>
        Fixed_Relative,
    }
}
