using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniverseSimulator
{
    /// <summary>
    /// Performs simulation operations between particles.
    /// </summary>
    public class Simulation
    {
        public static Random RandomGen = new Random();

        /// <summary>
        /// Creates a set of particles with random initialized positions within a given grid size.
        /// </summary>
        public static List<Particle> Get_RandomParticleSet(int NumberParticles, double GridSize, int NumberDimensions, int NumberProperties)
        {
            //Create a new list of particles with a default capacity equal to the amount we want
            List<Particle> ReturnList = new List<Particle>(NumberParticles);

            //For each "slot" in the list:
            for (int i = 0; i < ReturnList.Capacity; i++)
            {
                Particle x = new Particle(NumberDimensions, NumberProperties); //Create a new particle
                x.Position = Get_RandomPosition(GridSize, NumberDimensions);   //Set it to a random position
                ReturnList.Add(x);                                             //Add it to the list
            }

            //Return the finished list of particles
            return ReturnList;
        }

        /// <summary>
        /// Returns a position vector randomized to a particular grid space
        /// </summary>
        public static List<double> Get_RandomPosition(double GridSize, int NumberDimensions)
        {
            //Create a new list of position components with an appropriate default capacity
            List<double> ReturnList = new List<double>(NumberDimensions);

            //For each "slot" in the list:
            for (int i = 0; i < ReturnList.Capacity; i++)
            {
                ReturnList.Add(GenerateRandomDouble(-GridSize, GridSize));
            }

            //Return the finished list
            return ReturnList;
        }


        /// <summary>
        /// Returns a random double value between two double values
        /// </summary>
        public static double GenerateRandomDouble(double Lowerbound, double Upperbound)
        {
            return RandomGen.NextDouble() * (Upperbound - Lowerbound) + Lowerbound;
        }

        /// <summary>
        /// Updates a single particle.
        /// </summary>
        public static Particle UpdateParticle(Particle Input, List<Particle> ParticleList, double Precision, double SofteningValue, double GravityConstant, double MaxVelocity)
        {
            //Create a copy of the input particle that we can return without modifying the origional
            Particle ReturnParticle = Particle.Copy(Input);

            //First thing, we need to calculate acceleration:
            foreach (Particle Comparison in ParticleList)
            {
                //This will add each change in acceleration to the acceleration for each particle
                ReturnParticle.Acceleration = AddLists(ReturnParticle.Acceleration, CalculateGravitationalAcceleration(ReturnParticle, Comparison, Precision, SofteningValue, GravityConstant));
            }

            //Here we multiply by 0.0001 to account of the max timer speed
            ReturnParticle.Acceleration = MultiplyListByScalar(ReturnParticle.Acceleration, 0.0001);

            //Now we need to calculate velocity
            ReturnParticle.Velocity = AddLists(ReturnParticle.Velocity, ReturnParticle.Acceleration);

            //Check to see if anything goes over the max velocity
            double VelMag = VectorMagnitude(ReturnParticle.Velocity);
            if (VelMag > MaxVelocity)
            {
                for (int i = 0; i < ReturnParticle.Velocity.Capacity; i++)
                {
                    //The new velocity is equal to the old direction multiplied by the new magnitude
                    ReturnParticle.Velocity[i] = (ReturnParticle.Velocity[i] / VelMag) * MaxVelocity;
                }
            }
            

            //Now we calculate position and we're done (Note the multiplication by 0.0001 to account for the max timer speed - NOT NEEDED)
            ReturnParticle.Position = AddLists(ReturnParticle.Position, ReturnParticle.Velocity);

            return ReturnParticle;
        }

        /// <summary>
        /// Calculates the CHANGE in acceleration from one particle acting on another.
        /// </summary>
        public static List<double> CalculateGravitationalAcceleration(Particle Input, Particle Comparison, double Precision, double SofteningValue, double GravityConstant)
        {
            //First we create a new list of acceleration components with the same capacity of the input
            List<double> Acceleration = new List<double>(Input.Acceleration.Capacity);

            //Now we calculate the actual distance between the particles (we will need this later)
            double ActualDistance = CalculateActualDistance(Input.Position, Comparison.Position);

            //Check to see if the actual distance is under the precision value
            if (Math.Abs(ActualDistance) <= Precision)
            {
                //If it is, then we can just return a list with zero change
                for (int i = 0; i < Acceleration.Capacity; i++)
                {
                    Acceleration.Add(0);
                }
                return Acceleration;
            }

            //Calculate the denominator of the equation
            double Denominator = Math.Pow(((ActualDistance * ActualDistance) + (SofteningValue * SofteningValue)), (3 / 2));

            //For each dimension of movement/location:
            for (int i = 0; i < Acceleration.Capacity; i++)
            {
                //Calculate the component distance of this dimension
                double ComponentDistance = Comparison.Position[i] - Input.Position[i];

                //Calculate the change in acceleration
                Acceleration.Add(GravityConstant * ((Comparison.Properties[0] * ComponentDistance) / Denominator));
            }

            return Acceleration;
        }

        /// <summary>
        /// Calculates the actual distance between two particles.
        /// </summary>
        public static double CalculateActualDistance(List<double> InputPosition, List<double> ComparisonPosition)
        {
            //Create a new double to hold the radical summation
            double RadicalSum = 0;
            
            //Add the squares of the differences of the components
            for (int i = 0; i < InputPosition.Capacity; i++)
            {
                RadicalSum += ((ComparisonPosition[i] - InputPosition[i]) * (ComparisonPosition[i] - InputPosition[i]));
            }

            //Take the square root of this sum and return it
            return Math.Sqrt(RadicalSum);
        }

        /// <summary>
        /// Adds two lists together.
        /// </summary>
        public static List<double> AddLists(List<double> List1, List<double> List2)
        {
            //Create a new list to hold the new data
            List<double> NewList = new List<double>(List1.Capacity);

            //For each "slot" in the new list:
            for (int i = 0; i < NewList.Capacity; i++)
            {
                NewList.Add(List1[i] + List2[i]); //Add the previous two lists together
            }

            //Return the result
            return NewList;
        }

        /// <summary>
        /// Multiplies a list by a scalar amount.
        /// </summary>
        public static List<double> MultiplyListByScalar(List<double> List1, double Scalar)
        {
            //Create a new list to hold the new data
            List<double> NewList = new List<double>(List1.Capacity);

            //For each "slot" in the new list:
            for (int i = 0; i < NewList.Capacity; i++)
            {
                NewList.Add(List1[i] * Scalar); //Multiply
            }

            //Return the result
            return NewList;
        }

        public static List<double> CalculateCenterOfPosition(List<Particle> ParticleList)
        {
            //Create a new list to hold the result vector
            List<double> Result = new List<double>(ParticleList[0].Position.Capacity);
            
            for (int i = 0; i < Result.Capacity; i++)
            {
                Result.Add(0);
            }

            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                for (int j = 0; j < Result.Capacity; j++)
                {
                    Result[j] += ParticleList[i].Position[j];
                }
            }

            for (int i = 0; i < Result.Capacity; i++)
            {
                Result[i] = Result[i] / ParticleList.Count;
            }

            //Return the result
            return Result;
        }

        public static List<double> CalculateAverageVelocity(List<Particle> ParticleList)
        {
            //Create a new list to store the result vector
            List<double> Result = new List<double>(ParticleList[0].Velocity.Capacity);

            //Initialize the result list
            for (int i = 0; i < Result.Capacity; i++)
            {
                Result.Add(0);
            }

            //For each particle in the list:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //For each dimension:
                for (int j = 0; j < Result.Capacity; j++)
                {
                    Result[j] += ParticleList[i].Velocity[j];
                }
            }

            //Divide each dimension in the result list by the number of particles in the system
            for (int i = 0; i < Result.Capacity; i++)
            {
                Result[i] = Result[i] / ParticleList.Count;
            }

            //Return the result
            return Result;
        }

        public static List<double> CalculateAverageAcceleration(List<Particle> ParticleList)
        {
            //Create a new list to store the result vector
            List<double> Result = new List<double>(ParticleList[0].Acceleration.Capacity);

            //Initialize the result list
            for (int i = 0; i < Result.Capacity; i++)
            {
                Result.Add(0);
            }

            //For each particle:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //For each dimension:
                for (int j = 0; j < Result.Capacity; j++)
                {
                    Result[j] += ParticleList[i].Acceleration[j];
                }
            }

            //Divide the components of the result list by the number of particles in the system
            for (int i = 0; i < Result.Capacity; i++)
            {
                Result[i] = Result[i] / ParticleList.Count;
            }

            //Return the result
            return Result;
        }

        public static double CalculateAverageMass(List<Particle> ParticleList)
        {
            double Result = 0;

            //For every particle in the particle list:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //Add its mass to the result
                Result += ParticleList[i].Properties[0];
            }

            //Return the summed result divided by the number of particles in the list
            return (Result / ParticleList.Count);
        }

        public static double CalculateTotalMass(List<Particle> ParticleList)
        {
            double Result = 0;

            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                Result += ParticleList[i].Properties[0];
            }

            return Result;
        }

        /// <summary>
        /// Checks a list of particles for collisions and combines particles as neccessary.
        /// PERFECTLY INELASTIC
        /// </summary>
        public static List<Particle> CheckCollisions_PIE(List<Particle> InputList, double Precision, double CollisionDivider)
        {
            List<Particle> NewList = new List<Particle>(InputList.Capacity);
            List<int> CollidedIndicies = new List<int>();

            //For each particle:
            for (int i = 0; i < InputList.Count; i++)
            {
                //Check to see if this item has already been collided
                if (CollidedIndicies.Count > 0)
                {
                    if (CollidedIndicies.Contains(i))
                    {
                        //If this item is in the collided indicies, we need to skip it
                        continue;
                    }
                }

                Particle Temp = Particle.Copy(InputList[i]);

                //Otherwise for every other particle:
                for (int j = 0; j < InputList.Count; j++)
                {
                    if (InputList[i] == InputList[j]) continue; //If this is the same particle, skip it.

                    //If this particle and the other one has collided:
                    if (CalculateActualDistance(InputList[i].Position, InputList[j].Position) < (Precision / CollisionDivider))
                    {
                        //Add its index to the collided indicies
                        CollidedIndicies.Add(j);

                        //Calculate the collision of the new particle
                        Temp.Properties[0] += InputList[j].Properties[0];

                        //Calculate the new velocity of the particle
                        for (int k = 0; k < Temp.Velocity.Capacity; k++)
                        {
                            Temp.Velocity[k] = (InputList[i].Velocity[k] * InputList[i].Properties[0]) + (InputList[j].Velocity[k] * InputList[j].Properties[0]);
                            Temp.Velocity[k] /= Temp.Properties[0];
                        }
                    }
                }

                //Add the new particle to the temp list
                NewList.Add(Temp);
            }

            //Clean up the list
            NewList.Capacity = NewList.Count;

            return NewList;
        }

        public static List<double> CalculateElectrostaticAcceleration(Particle Input, Particle Comparison, double SofteningValue, double ElectrostaticConstant)
        {
            //First we create a new list of acceleration components with the same capacity of the input
            List<double> Acceleration = new List<double>(Input.Acceleration.Capacity);

            //Now we calculate the actual distance between the particles (we will need this later)
            double ActualDistance = CalculateActualDistance(Input.Position, Comparison.Position);

            //Since we are dealing with electrostatics, we only want to worry about distances equal to zero
            if (Math.Abs(ActualDistance) == 0)
            {
                //If it is, then we can just return a list with zero change
                for (int i = 0; i < Acceleration.Capacity; i++)
                {
                    Acceleration.Add(0);
                }
                return Acceleration;
            }

            //Calculate the denominator of the equation
            double Denominator = Math.Pow(((ActualDistance * ActualDistance) + (SofteningValue * SofteningValue)), (3 / 2));

            //For each dimension of movement/location:
            for (int i = 0; i < Acceleration.Capacity; i++)
            {
                //Calculate the component distance of this dimension
                double ComponentDistance = Comparison.Position[i] - Input.Position[i];

                //Calculate the change in acceleration
                Acceleration.Add((ElectrostaticConstant * ((Comparison.Properties[1] * Input.Properties[1] * ComponentDistance) / Denominator)) / Input.Properties[0]);
            }

            return Acceleration;
        }

        /// <summary>
        /// Re-centers a simulation so that the central point becomes zero.
        /// </summary>
        public static List<Particle> ReCenterSimulation(List<Particle> InputList)
        {
            //Create a new list of particles
            List<Particle> CenteredList = new List<Particle>(InputList.Capacity);

            //Get the center of the list of particles
            List<double> Center = CalculateCenterOfPosition(InputList);

            //For each particle in the list:
            foreach (Particle x in InputList)
            {
                //Create a copy of that particle
                Particle Temp = Particle.Copy(x);

                //For each dimension of that particle
                for (int i = 0; i < Center.Capacity; i++)
                {
                    //Change the position of the temp particle
                    Temp.Position[i] -= Center[i];
                }

                //Add that particle to the output list
                CenteredList.Add(Temp);
            }


            //Center the particles
            return CenteredList;
        }

        /// <summary>
        /// Updates a list of particles given certain conditions using threading.
        /// </summary>
        public static List<Particle> Threaded_UpdateList(List<Particle> ParticleList, double Precision, double SofteningValue, double GravityConstant, double MaxVelocity)
        {
            //If the particle list is empty, then  lets just stop
            if (ParticleList.Count < 1)
            {
                return ParticleList;
            }

            //Create a new particle list to hold the new values
            List<Particle> NewList = new List<Particle>(ParticleList.Capacity);

            //Pump the list with null values
            for (int i = 0; i < NewList.Capacity; i++)
            {
                NewList.Add(null);
            }

            //Calculate everything in parallel
            Parallel.For(0, ParticleList.Capacity, i =>
            {
                Particle New = UpdateParticle(ParticleList[i], ParticleList, Precision, SofteningValue, GravityConstant, MaxVelocity);
                
                //We want to keep the particles in order, so lets wait for our slot to open in the new list
                
                lock (NewList) //Used to prevent 2 threads from accessing NewList at the same time
                {
                    NewList[i] = New;
                }
            });

            //Clean up the list
            NewList.Capacity = NewList.Count;

            //Return the list
            return NewList;
        }

        public static List<Particle> Threaded_CheckCollisions(List<Particle> InputList, double Precision, double CollisionDivider)
        {
            List<Particle> NewList = new List<Particle>(InputList.Capacity);
            List<int> CollidedIndicies = new List<int>();

            //For each particle:
            Parallel.For(0, InputList.Count, i =>
            {
                //Check to see if this item has already been collided
                if (CollidedIndicies.Contains(i))
                {
                    //Do nothing
                }
                else
                {
                    Particle Temp = Particle.Copy(InputList[i]);

                    //Otherwise for every other particle:
                    for (int j = 0; j < InputList.Count; j++)
                    {
                        if (InputList[i] == InputList[j]) continue; //If this is the same particle, skip it.

                        //If this particle and the other one has collided:
                        if (CalculateActualDistance(InputList[i].Position, InputList[j].Position) < (Precision / CollisionDivider))
                        {
                            //Add its index to the collided indicies
                            CollidedIndicies.Add(j);

                            //Calculate the collision of the new particle
                            Temp.Properties[0] += InputList[j].Properties[0];

                            //Calculate the new velocity of the particle
                            for (int k = 0; k < Temp.Velocity.Capacity; k++)
                            {
                                Temp.Velocity[k] = (InputList[i].Velocity[k] * InputList[i].Properties[0]) + (InputList[j].Velocity[k] * InputList[j].Properties[0]);
                                Temp.Velocity[k] /= Temp.Properties[0];
                            }
                        }
                    }

                    //Add the new particle to the temp list
                    lock (NewList)
                    {
                        NewList.Add(Temp);
                    }
                }
            });

            //Clean up the list
            NewList.Capacity = NewList.Count;

            return NewList;
        }

        /// <summary>
        /// Updates a list of particles according to newtonian gravitation utilizing the GPU and multi-threading.
        /// </summary>
        public static List<Particle> GPU_UpdateList(List<Particle> ParticleList, double Precision, double SofteningValue, double GravityConstant, double MaxVelocity)
        {
            //If the particle list is empty, then  lets just stop
            if (ParticleList.Count < 1)
            {
                return ParticleList;
            }

            //Create a new particle list to hold the new values
            List<Particle> NewList = new List<Particle>(ParticleList.Capacity);

            //Calculate everything in parallel
            Parallel.For(0, ParticleList.Capacity, i =>
            {
                Particle New = UpdateParticle(ParticleList[i], ParticleList, Precision, SofteningValue, GravityConstant, MaxVelocity);
                lock (NewList) //Used to prevent 2 threads from accessing NewList at the same time
                {
                    NewList.Add(New);
                }
            });

            //Clean up the list
            NewList.Capacity = NewList.Count;

            //Return the list
            return NewList;
        }

        /// <summary>
        /// Reterns a random set of particles seeded to random starting positions within a circle of a set size.
        /// Only supports 2 dimensions
        /// </summary>
        public static List<Particle> Get_RandomCircularParticleSet(int NumberParticles, double GridSize, int NumberProperties)
        {
            //Create a new list of particles with a default capacity equal to the amount we want
            List<Particle> ReturnList = new List<Particle>(NumberParticles);

            //For each "slot" in the list:
            for (int i = 0; i < ReturnList.Capacity; i++)
            {
                Particle x = new Particle(2, NumberProperties); //Create a new particle
                x.Position = Get_RandomCircularPosition(GridSize);   //Set it to a random position
                ReturnList.Add(x);                                             //Add it to the list
            }

            //Return the finished list of particles
            return ReturnList;
        }

        public static List<double> Get_RandomCircularPosition(double GridSize)
        {
            //Create a new list to contain the coordinates
            List<double> ReturnPos = new List<double>(2);

            //Calculate the angle and distance of the coordinate
            double Angle = 2.0 * Math.PI * RandomGen.NextDouble();
            double Dist = GenerateRandomDouble(0, GridSize);

            //Caclulate the rectangular coordinates
            double X = Dist * Math.Cos(Angle);
            ReturnPos.Add(X);
            double Y = Dist * Math.Sin(Angle);
            ReturnPos.Add(Y);

            return ReturnPos;
        }

        /// <summary>
        /// Checks a list of particles for collisions and combines particles as neccessary.
        /// </summary>
        public static List<Particle> CheckCollisions(List<Particle> InputList, double Precision, double CollisionDivider, CollisionMode CollisionType)
        {
            if (CollisionType == CollisionMode.PerfectlyInelastic) return CheckCollisions_PIE(InputList, Precision, CollisionDivider);
            if (CollisionType == CollisionMode.PerfectlyElastic) return CheckCollisions_PE(InputList, Precision, CollisionDivider);

            return null;
        }

        /// <summary>
        /// Checks a list of particles for collisions and combines particles as neccessary.
        /// PERFECTLY ELASTIC
        /// </summary>
        public static List<Particle> CheckCollisions_PE(List<Particle> InputList, double Precision, double CollisionDivider)
        {
            List<Particle> NewList = new List<Particle>(InputList.Capacity);

            //For each particle:
            for (int i = 0; i < InputList.Count; i++)
            {
                Particle Temp = Particle.Copy(InputList[i]);

                //For every other particle:
                for (int j = 0; j < InputList.Count; j++)
                {
                    if (InputList[i] == InputList[j]) continue; //If this is the same particle, skip it.

                    double AD = CalculateActualDistance(InputList[i].Position, InputList[j].Position);

                    //If this particle and the other one has collided:
                    if (AD < (Precision / CollisionDivider))
                    {
                        //Get the magnitude of the velocity
                        double VelocityMagnitude = VectorMagnitude(InputList[i].Velocity);

                        //Calculate the new velocity of the particle
                        for (int k = 0; k < Temp.Velocity.Capacity; k++)
                        {
                            //Calculate the unit vector of the distance in this dimension (IE the direction to the particle)
                            double Direction = (InputList[j].Position[k] - InputList[i].Position[k]) / AD;

                            //Set the velocity to its magnitude multiplied by the new direction
                            Temp.Velocity[k] = VelocityMagnitude * (-1.0 * Direction);
                        }
                    }
                }

                //Add the new particle to the temp list
                NewList.Add(Temp);
            }

            //Clean up the list
            NewList.Capacity = NewList.Count;

            return NewList;
        }

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        public static double VectorMagnitude(List<double> InputVector)
        {
            //Create a new double to hold the radical summation
            double RadicalSum = 0;

            //Add the squares of the of the components
            for (int i = 0; i < InputVector.Capacity; i++)
            {
                RadicalSum += (InputVector[i] * InputVector[i]);
            }

            //Take the square root of this sum and return it
            return Math.Sqrt(RadicalSum);
        }

        /// <summary>
        /// Checks a list of particles for boundary violations and updates the list.
        /// </summary>
        public static List<Particle> CheckBoundaries(List<Particle> ParticleList, UniverseBoundaryType BoundaryType, double UniverseSize)
        {
            switch (BoundaryType)
            {
                case UniverseBoundaryType.Infinate:
                    return ParticleList; //We don't need to do anything, so let's just return the list.
                case UniverseBoundaryType.Fixed_HardEdge:
                    return CheckBoundaries_Fixed_HardEdge(ParticleList, UniverseSize);
                case UniverseBoundaryType.Fixed_Relative:
                    return CheckBoundaries_Fixed_Relative(ParticleList, UniverseSize);
                case UniverseBoundaryType.Fixed_Wrapped:
                    return CheckBoundaries_Fixed_Wrapped(ParticleList, UniverseSize);
                default:
                    return ParticleList; //Assume infinate
            }
        }

        public static List<Particle> CheckBoundaries_Fixed_HardEdge(List<Particle> ParticleList, double UniverseSize)
        {
            //Create a new list to hold the updated values
            List<Particle> NewList = new List<Particle>(ParticleList.Capacity);

            //For each particle:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //Copy the particle
                Particle Temp = Particle.Copy(ParticleList[i]);

                //For each dimension
                for (int j = 0; j < Temp.Position.Capacity; j++)
                {
                    //If the dimension is greater than the universe size:
                    if (Temp.Position[j] > UniverseSize)
                    {
                        Temp.Position[j] = UniverseSize;
                    }
                    else if (Temp.Position[j] < -UniverseSize)
                    {
                        Temp.Position[j] = -UniverseSize;
                    }
                    Temp.Velocity[j] = 0;
                }
                NewList.Add(Temp);
            }

            //Return the list
            return NewList;
        }

        public static List<Particle> CheckBoundaries_Fixed_Relative(List<Particle> ParticleList, double UniverseSize)
        {
            return null;
        }

        public static List<Particle> CheckBoundaries_Fixed_Wrapped(List<Particle> ParticleList, double UniverseSize)
        {
            //Create a new list to hold the updated values
            List<Particle> NewList = new List<Particle>(ParticleList.Capacity);

            //For each particle:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //Copy the particle
                Particle Temp = Particle.Copy(ParticleList[i]);

                //For each dimension
                for (int j = 0; j < Temp.Position.Capacity; j++)
                {
                    //If the dimension is greater than the universe size:
                    if (Temp.Position[j] >= UniverseSize)
                    {
                        double difference = Math.Abs(Temp.Position[j] - UniverseSize);
                        Temp.Position[j] = -UniverseSize + difference;
                    }
                    else if (Temp.Position[j] <= -UniverseSize)
                    {
                        double difference = Math.Abs(Temp.Position[j] - UniverseSize);
                        Temp.Position[j] = UniverseSize - difference;
                    }
                    
                }
                NewList.Add(Temp);
            }

            //Return the list
            return NewList;
        }

        /// <summary>
        /// Converts a list of doubles to a list of floats
        /// </summary>
        public static List<float> ConvertDoubleListToFloatList(List<double> Input)
        {
            List<float> ReturnList = new List<float>(Input.Capacity);

            foreach (double x in Input)
            {
                ReturnList.Add((float)Math.Round(x, 4));
            }

            return ReturnList;
        }
    }
}
