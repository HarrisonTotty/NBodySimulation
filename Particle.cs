using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UniverseSimulator
{
    /// <summary>
    /// Represents a particle.
    /// </summary>
    [Serializable()]
    public class Particle
    {
        public Particle(int NumberDimensions, int NumberProperties)
        {
            //Create all of the lists
            this.Position = new List<double>(NumberDimensions);
            this.Velocity = new List<double>(NumberDimensions);
            this.Acceleration = new List<double>(NumberDimensions);
            this.Properties = new List<double>(NumberProperties);

            //Initialize all of the values to zero
            for (int i = 0; i < NumberDimensions; i++)
            {
                this.Position.Add(0);
                this.Velocity.Add(0);
                this.Acceleration.Add(0);
            }
            for (int i = 0; i < NumberProperties; i++)
            {
                this.Properties.Add(0);
            }
        }

        /// <summary>
        /// Contains the component information about the position of a particle in each index.
        /// </summary>
        public List<double> Position
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the component information about the velocity of a particle in each index.
        /// </summary>
        public List<double> Velocity
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the component information about the acceleration of a particle in each index.
        /// </summary>
        public List<double> Acceleration
        {
            get;
            set;
        }

        /// <summary>
        /// Contains information about properties such as a particle's mass and/or charge in each index.
        /// </summary>
        public List<double> Properties
        {
            get;
            set;
        }

        /// <summary>
        /// Copies the information from a particle to a new one.
        /// </summary>
        public static Particle Copy(Particle Input)
        {
            Particle ReturnParticle = new Particle(Input.Acceleration.Capacity, Input.Properties.Capacity);

            for (int i = 0; i < Input.Acceleration.Capacity; i++)
            {
                ReturnParticle.Acceleration[i] = Input.Acceleration[i];
                ReturnParticle.Velocity[i] = Input.Velocity[i];
                ReturnParticle.Position[i] = Input.Position[i];
            }
            

            for (int i = 0; i < Input.Properties.Capacity; i++)
            {
                ReturnParticle.Properties[i] = Input.Properties[i];
            }
            

            return ReturnParticle;
        }
    }
}
