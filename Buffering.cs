using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UniverseSimulator
{
    public partial class Buffering : Form
    {
        public static List<Particle> ParticleList;
        public static int CurrentGen = 0;
        public static int MaxGen;
        public static long TotalCalculations = 0;

        //SETTINGS
        public static double Precision;
        public static double SofteningValue;
        public static double MaxVelocity;
        public static double GravityConstant;
        public static bool Collisions;
        public static double CollisionsDivider;
        public static CollisionMode CollisionType;
        public static UniverseBoundaryType BoundaryType;
        public static double UniverseSize;

        public Buffering(List<Particle> SeedList, double Prec, double Soft, double MaxV,  double Grav, bool Coll, double CollD, CollisionMode CM, UniverseBoundaryType BT, double Size)
        {
            InitializeComponent();
            ParticleList = SeedList;
            Precision = Prec;
            SofteningValue = Soft;
            MaxVelocity = MaxV;
            GravityConstant = Grav;
            Collisions = Coll;
            CollisionsDivider = CollD;
            CollisionType = CM;
            BoundaryType = BT;
            UniverseSize = Size;
        }

        private void Buffering_Load(object sender, EventArgs e)
        {
            TotalCalculations = 0;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            NumGen.Enabled = false;
            label1.Enabled = false;
            
            MaxGen = Convert.ToInt32(NumGen.Value);
            PBar.Maximum = MaxGen;
            BufferedSimulation Sim = new BufferedSimulation(MaxGen);
            
            //For each generation
            for (CurrentGen = 0; CurrentGen < MaxGen; CurrentGen++)
            {
                //Perform the calculation on that generation
                ParticleList = Simulation.Threaded_UpdateList(ParticleList, Precision, SofteningValue, GravityConstant, MaxVelocity);

                //Update the number of calculations
                TotalCalculations += ParticleList.Count * ParticleList.Count * 2;
                Status.Clear();
                Status.AppendText("Performed " + TotalCalculations.ToString("n0") + " Calculations...");

                Thread.Sleep(0);

                //Check for collisions!
                if (Collisions == true)
                {
                    ParticleList = Simulation.CheckCollisions(ParticleList, Precision, CollisionsDivider, CollisionType);

                    //Update the number of calculations
                    TotalCalculations += ParticleList.Count * ParticleList.Count * 2;
                    Status.Clear();
                    Status.AppendText("Performed " + TotalCalculations.ToString("n0") + " Calculations...");
                }


                //Check for boundaries!
                ParticleList = Simulation.CheckBoundaries(ParticleList, BoundaryType, UniverseSize);

                //Update the title
                double Percent = ((double)CurrentGen / (double)MaxGen) * 100;
                this.Text = "Buffering (" + Percent.ToString("n2") + "% Complete)...";

                //Add the set to the generation count
                Sim.AddGeneration(ParticleList);

                PBar.Value++;
                Thread.Sleep(0);
            }

            BufferedViewer x = new BufferedViewer(Sim);
            x.Show();
            this.Close();
        }

    }
}
