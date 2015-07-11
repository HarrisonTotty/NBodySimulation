using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UniverseSimulator
{
    /// <summary>
    /// Deals with object serialization.
    /// </summary>
    public class Serialization
    {
        public static void SerializeParticleList(string filename, List<Particle> ParticleList)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, ParticleList);
            stream.Close();
        }

        public static List<Particle> DeserializeParticleList(string filename)
        {
            List<Particle> objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (List<Particle>)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
