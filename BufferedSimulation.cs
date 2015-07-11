using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UniverseSimulator
{
    [Serializable()]
    public class BufferedSimulation
    {
        public BufferedSimulation(int NumberGenerations)
        {
            this.Data = new List<List<List<double>>>(NumberGenerations);
        }


        //Data[GENERATION][PARTICLE][DIMENSION]
        public List<List<List<double>>> Data
        {
            get;
            set;
        }

        public void AddGeneration(List<Particle> ParticleList)
        {
            List<List<double>> NewParticleSet = new List<List<double>>(ParticleList.Capacity);

            foreach (Particle x in ParticleList)
            {
                NewParticleSet.Add(x.Position);
            }

            this.Data.Add(NewParticleSet);
        }
    }
}
