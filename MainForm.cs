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
    public partial class MainForm : Form
    {
        //Particle list and current generation
        public static List<Particle> ParticleList = new List<Particle>();
        public static int GenerationCount = 0;

        //CONSTANTS
        public static double GRAVITY = 6.67384E-11;

        //------------SETTINGS--------------
        //Basic and Universe
        public static double Precision = 0.01;
        public static double SofteningValue = 5;
        public static double MaxVelocity = 100;
        public static double GravityConstant = GRAVITY * 1.0E8;
        public static UniverseBoundaryType BoundaryType = UniverseBoundaryType.Infinate;
        public static double UniverseScale = 500;
        //Collisions
        public static bool Collisions = true;
        public static double CollisionsDivider = 2;
        public static CollisionMode CollisionType = CollisionMode.PerfectlyInelastic;
        //Clicking
        public static double DropWeight = 10;
        //Processing and Rendering
        public static bool ParallelProcessing = true;
        public static bool RenderOnlyVisible = true;
        public static bool SelectiveRenderingOn = false;
        public static int SelectiveRenderResolution = 250;
        //Seeding
        public static SeedType ThisSeedType = SeedType.Rectangular;
        public static bool IsCrazyMassSeed = false;
        //Time Graph Visualization
        public static bool TimeGraphsOn = false;
        public static TimeChartRangeType TimeChartRange = TimeChartRangeType.All;
        public static TCDisplayedType Chart1Type = TCDisplayedType.AverageAcceleration;
        public static TCDisplayedType Chart2Type = TCDisplayedType.AverageVelocity;
        public static int TimeGraphResolution = -1;
        public static List<double> AverageAccelerations;
        public static List<double> AverageVelocities;
        public static List<double> SpecificAccelerations;
        public static List<double> SpecificVelocities;
        public static int SpecificIndex = 0;
        public static List<int> GenerationIndex;
        public static List<int> SpecificGenerationIndex;
        public static int LastStartIndex = 0;
        //Jam prevention
        public static bool TrackBarChanging = false;
        //----------------------------------

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Manipulation_Setup();
        }

        /// <summary>
        /// Redraws the trackbar and power selection boxes when switching manipulation modes
        /// </summary>
        private void Manipulation_SetTrackBar()
        {
            bool wasrunning = SimTimer.Enabled;
            if (wasrunning) SimTimer.Stop();
            TrackBarChanging = true;
            switch (ManipulateBox.SelectedItem.ToString().ToLower())
            {
                case "gravitational constant":
                    Manipulation_SetupGravityConstant();
                    break;
                case "calculation precision":
                    Manipulation_SetupCalculationPrecision();
                    break;
                case "collision precision":
                    Manipulation_SetupCollisionPrecision();
                    break;
                case "maximum velocity":
                    Manipulation_SetupMaximumVelocity();
                    break;
                case "softening value":
                    Manipulation_SetupSofteningValue();
                    break;
                default:
                    break;
            }
            TrackBarChanging = false;
            if (wasrunning) SimTimer.Start();
        }

        private void Manipulation_SetupGravityConstant()
        {
            //Setup the available exponents
            ManipulateExponent.Items.Clear();
            ManipulateExponent.Enabled = true;
            ManipulateExponent.Items.Add("10^-11 (Natural)");
            ManipulateExponent.Items.Add("10^-9");
            ManipulateExponent.Items.Add("10^-7");
            ManipulateExponent.Items.Add("10^-5");
            ManipulateExponent.Items.Add("10^-4");
            ManipulateExponent.Items.Add("10^-3 (Default)");
            ManipulateExponent.Items.Add("0.01");
            ManipulateExponent.Items.Add("0.1");
            ManipulateExponent.Items.Add("1");
            ManipulateExponent.Items.Add("10");
            ManipulateExponent.Items.Add("100");

            double NonScientificNumber = 0;

            //Figure out the current exponant
            if (GravityConstant >= 100)
            {
                ManipulateExponent.SelectedItem = "100";
                NonScientificNumber = GravityConstant / 100;
            }
            else if (GravityConstant >= 10)
            {
                ManipulateExponent.SelectedItem = "10";
                NonScientificNumber = GravityConstant / 10;
            }
            else if (GravityConstant >= 1)
            {
                ManipulateExponent.SelectedItem = "1";
                NonScientificNumber = GravityConstant;
            }
            else if (GravityConstant >= 0.1)
            {
                ManipulateExponent.SelectedItem = "0.1";
                NonScientificNumber = GravityConstant / 0.1;
            }
            else if (GravityConstant >= 0.01)
            {
                ManipulateExponent.SelectedItem = "0.01";
                NonScientificNumber = GravityConstant / 0.01;
            }
            else if (GravityConstant >= 1.0E-3)
            {
                ManipulateExponent.SelectedItem = "10^-3 (Default)";
                NonScientificNumber = GravityConstant / 1.0E-3;
            }
            else if (GravityConstant >= 1.0E-4)
            {
                ManipulateExponent.SelectedItem = "10^-4";
                NonScientificNumber = GravityConstant / 1.0E-4;
            }
            else if (GravityConstant >= 1.0E-5)
            {
                ManipulateExponent.SelectedItem = "10^-5";
                NonScientificNumber = GravityConstant / 1.0E-5;
            }
            else if (GravityConstant >= 1.0E-7)
            {
                ManipulateExponent.SelectedItem = "10^-7";
                NonScientificNumber = GravityConstant / 1.0E-7;
            }
            else if (GravityConstant >= 1.0E-9)
            {
                ManipulateExponent.SelectedItem = "10^-9";
                NonScientificNumber = GravityConstant / 1.0E-9;
            }
            else if (GravityConstant >= 1.0E-11)
            {
                ManipulateExponent.SelectedItem = "10^-11 (Natural)";
                NonScientificNumber = GravityConstant / 1.0E-11;
            }

            //Setup the available trackbar values (to get the actual value to display in the TB, divide by 100)
            ManipulateTrackBar.Minimum = 100;
            ManipulateTrackBar.Maximum = 1000;

            //Set the trackbar and text box text
            ManipulateTrackBar.Value = (int)(NonScientificNumber * 100);
            ManipulateTB.Text = ((double)ManipulateTrackBar.Value / 100.0).ToString("n2") + "  X";
        }

        private void Manipulation_SetupCollisionPrecision()
        {
            //Setup the available exponents
            ManipulateExponent.Items.Clear();
            ManipulateExponent.Enabled = false;

            //Setup the available trackbar values (to get the actual value to display in the TB, divide by 100)
            ManipulateTrackBar.Minimum = 1;
            ManipulateTrackBar.Maximum = 1000;

            //Set the trackbar and text box text
            ManipulateTrackBar.Value = (int)(CollisionsDivider * 100);
            ManipulateTB.Text = ((double)ManipulateTrackBar.Value / 100.0).ToString("n2");
        }

        private void Manipulation_SetupCalculationPrecision()
        {
            //Setup the available exponents
            ManipulateExponent.Items.Clear();
            ManipulateExponent.Enabled = false;

            //Setup the available trackbar values (to get the actual value to display in the TB, divide by 1000)
            ManipulateTrackBar.Minimum = 1;
            ManipulateTrackBar.Maximum = 10000;

            //Set the trackbar and text box text
            ManipulateTrackBar.Value = (int)(Precision * 1000);
            ManipulateTB.Text = ((double)ManipulateTrackBar.Value / 1000.0).ToString("n3");
        }

        private void Manipulation_SetupSofteningValue()
        {
            //Setup the available exponents
            ManipulateExponent.Items.Clear();
            ManipulateExponent.Enabled = false;

            //Setup the available trackbar values (to get the actual value to display in the TB, divide by 10)
            ManipulateTrackBar.Minimum = 1;
            ManipulateTrackBar.Maximum = 1000;

            //Set the trackbar and text box text
            ManipulateTrackBar.Value = (int)(SofteningValue * 10);
            ManipulateTB.Text = ((double)ManipulateTrackBar.Value / 10.0).ToString("n1");
        }

        private void Manipulation_SetupMaximumVelocity()
        {
            //Setup the available exponents
            ManipulateExponent.Items.Clear();
            ManipulateExponent.Enabled = false;

            //Setup the available trackbar values (to get the actual value to display in the TB, divide by 1000)
            ManipulateTrackBar.Minimum = 1;
            ManipulateTrackBar.Maximum = 1000;

            //Set the trackbar and text box text
            ManipulateTrackBar.Value = (int)(MaxVelocity);
            ManipulateTB.Text = ((double)ManipulateTrackBar.Value).ToString();
        }

        /// <summary>
        /// Starts the form with the correctly loaded values
        /// </summary>
        private void Manipulation_Setup()
        {
            TrackBarChanging = true;
            ManipulateBox.SelectedItem = "Gravitational Constant";
            Manipulation_SetTrackBar();
        }

        private void SeedParticles(int ParticleCount, double StartSize)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(ParticleCount, StartSize, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(ParticleCount, StartSize, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                if (!IsCrazyMassSeed) ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
                if (IsCrazyMassSeed) ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(10, 100000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Tell the user we are ready
            string NL = Environment.NewLine;
            string MessageString = "Sucessfully seeded " + ParticleCount + " particles!" + NL + NL;
            MessageString += "Seed Type: " + ThisSeedType.ToString().ToUpper() + NL;
            if (IsCrazyMassSeed) MessageString += "Mass Type: CRAZY";
            if (!IsCrazyMassSeed) MessageString += "Mass Type: REGULAR";
            MessageBox.Show(MessageString, "Seed Successful!", MessageBoxButtons.OK);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            ParticleList = new List<Particle>();

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void SimTimer_Tick(object sender, EventArgs e)
        {
            //Stop the timer
            SimTimer.Stop();

            //If the particle list is empty, then  lets just stop
            if (ParticleList.Count < 1)
            {
                GenerationCount++;
                SimTimer.Start();
                return;
            }

            //If we are doing parallel processing, run the parallel method
            if (ParallelProcessing == true)
            {
                SimTimer_ParallelTick();
                return;
            } 

            //Create a new list of particles to hold the modified particles
            List<Particle> UpdatedList = new List<Particle>(ParticleList.Capacity);

            //Clear the Grid
            if (Tabs.SelectedTab == Tab_Grid && SelectiveRenderingOn == false) PrimaryGrid.Series[0].Points.Clear();
            if (Tabs.SelectedTab == Tab_Components)
            {
                ComponentsChart.Series[0].Points.Clear();
                ComponentsChart.Series[1].Points.Clear();
            }

            if (Tabs.SelectedTab == Tab_ParticleData) RawData.Items.Clear();

            //For each particle in the list:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //Calculate the updated properties of the particle and put it into the new list
                Particle New = Simulation.UpdateParticle(ParticleList[i], ParticleList, Precision, SofteningValue, GravityConstant, MaxVelocity);
                UpdatedList.Add(New);

                //Draw the particle
                if (Tabs.SelectedTab == Tab_Grid)
                {
                    if (RenderOnlyVisible)
                    {
                        if (IsInView(New.Position)) PrimaryGrid.Series[0].Points.AddXY(New.Position[0], New.Position[1]);
                    }
                    else
                    {
                        if (!SelectiveRenderingOn) PrimaryGrid.Series[0].Points.AddXY(New.Position[0], New.Position[1]);
                    }
                }
                
                if (Tabs.SelectedTab == Tab_Components)
                {
                    ComponentsChart.Series[0].Points.AddXY(New.Acceleration[0], New.Acceleration[1]);
                    ComponentsChart.Series[1].Points.AddXY(New.Velocity[0], New.Velocity[1]);
                }

                if (Tabs.SelectedTab == Tab_ParticleData) PrintRawData(New, i);
            }

            //Set the particle list to the updated list
            ParticleList = UpdatedList;

            //Selective Rendering!
            if (SelectiveRenderingOn) SelectiveRender();

            //Check for collisions!
            if (Collisions == true) ParticleList = Simulation.CheckCollisions(ParticleList, Precision, CollisionsDivider, CollisionType);

            //Check Boundaries!
            ParticleList = Simulation.CheckBoundaries(ParticleList, BoundaryType, UniverseScale);

            //Update the time charts!
            if (TimeGraphsOn) UpdateTimeCharts();

            //Increase the generation count and update it visually
            GenerationCount++;
            if (Tabs.SelectedTab == Tab_Grid) PrimaryGrid.Titles[0].Text = "Generation " + GenerationCount;
            if (Tabs.SelectedTab == Tab_Components) ComponentsChart.Titles[0].Text = "Generation " + GenerationCount;
            if (Tabs.SelectedTab == Tab_TimeGraphs && TimeGraphsOn) DrawTimeCharts();
            if (Tabs.SelectedTab == Tab_AddInfo) UpdateAdditionalInfo();

            //Start the timer
            SimTimer.Start();
        }

        private void SimTimer_ParallelTick()
        {
            //Update the particle sets
            ParticleList = Simulation.Threaded_UpdateList(ParticleList, Precision, SofteningValue, GravityConstant, MaxVelocity);

            //Clear the Grid
            if (Tabs.SelectedTab == Tab_Grid && SelectiveRenderingOn == false) PrimaryGrid.Series[0].Points.Clear();
            if (Tabs.SelectedTab == Tab_Components)
            {
                ComponentsChart.Series[0].Points.Clear();
                ComponentsChart.Series[1].Points.Clear();
            }
            if (Tabs.SelectedTab == Tab_ParticleData) RawData.Items.Clear();

            //Draw the particles
            if (SelectiveRenderingOn == false)
            {
                for (int i = 0; i < ParticleList.Capacity; i++)
                {
                    //Draw the particle
                    if (Tabs.SelectedTab == Tab_Grid)
                    {
                        if (RenderOnlyVisible)
                        {
                            if (IsInView(ParticleList[i].Position)) PrimaryGrid.Series[0].Points.AddXY(ParticleList[i].Position[0], ParticleList[i].Position[1]);
                        }
                        else
                        {
                            PrimaryGrid.Series[0].Points.AddXY(ParticleList[i].Position[0], ParticleList[i].Position[1]);
                        }
                    }

                    if (Tabs.SelectedTab == Tab_Components)
                    {
                        ComponentsChart.Series[0].Points.AddXY(ParticleList[i].Acceleration[0], ParticleList[i].Acceleration[1]);
                        ComponentsChart.Series[1].Points.AddXY(ParticleList[i].Velocity[0], ParticleList[i].Velocity[1]);
                    }

                    if (Tabs.SelectedTab == Tab_ParticleData) PrintRawData(ParticleList[i], i);
                }
            }
            else
            {
                SelectiveRender();
            }

            //Check for Collisions!
            if (Collisions == true) ParticleList = Simulation.CheckCollisions(ParticleList, Precision, CollisionsDivider, CollisionType);

            //Check Boundaries!
            ParticleList = Simulation.CheckBoundaries(ParticleList, BoundaryType, UniverseScale);
            
            //Update the time charts!
            if (TimeGraphsOn) UpdateTimeCharts();

            //Increase the generation count and update it visually
            GenerationCount++;
            if (Tabs.SelectedTab == Tab_Grid) PrimaryGrid.Titles[0].Text = "Generation " + GenerationCount;
            if (Tabs.SelectedTab == Tab_Components) ComponentsChart.Titles[0].Text = "Generation " + GenerationCount;
            if (Tabs.SelectedTab == Tab_AddInfo) UpdateAdditionalInfo();
            if (Tabs.SelectedTab == Tab_TimeGraphs && TimeGraphsOn) DrawTimeCharts();

            SimTimer.Start();
        }

        private void SelectiveRender()
        {
            //First clear the grid
            PrimaryGrid.Series[0].Points.Clear();

            //Now, determine the size of a slot
            double SlotSizeX = (PrimaryGrid.ChartAreas[0].AxisX.Maximum - PrimaryGrid.ChartAreas[0].AxisX.Minimum) / SelectiveRenderResolution;
            double SlotSizeY = (PrimaryGrid.ChartAreas[0].AxisY.Maximum - PrimaryGrid.ChartAreas[0].AxisY.Minimum) / SelectiveRenderResolution;

            //Now create an array to hold the slots
            bool[,] IsTaken = new bool[SelectiveRenderResolution, SelectiveRenderResolution];

            //For each particle in the list
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                //Determine what slot it goes in
                int LocX = (int)Math.Round((ParticleList[i].Position[0] - PrimaryGrid.ChartAreas[0].AxisX.Minimum) / SlotSizeX);
                int LocY = (int)Math.Round((ParticleList[i].Position[1] - PrimaryGrid.ChartAreas[0].AxisY.Minimum) / SlotSizeY);

                //Determine if we are off the grid
                if (LocX < 0 || LocX >= SelectiveRenderResolution) continue;
                if (LocY < 0 || LocY >= SelectiveRenderResolution) continue;

                //If the position isn't taken:
                if (IsTaken[LocX, LocY] == false)
                {
                    //Make it taken and draw the point
                    IsTaken[LocX, LocY] = true;
                    PrimaryGrid.Series[0].Points.AddXY(ParticleList[i].Position[0], ParticleList[i].Position[1]);
                }
                else
                {
                    continue;
                }
            }

        }

        private void UpdateTimeCharts()
        {
            AverageAccelerations.Add(Simulation.VectorMagnitude(Simulation.CalculateAverageAcceleration(ParticleList)));
            AverageVelocities.Add(Simulation.VectorMagnitude(Simulation.CalculateAverageVelocity(ParticleList)));
            SpecificAccelerations.Add(Simulation.VectorMagnitude(ParticleList[SpecificIndex].Acceleration));
            SpecificVelocities.Add(Simulation.VectorMagnitude(ParticleList[SpecificIndex].Velocity));
            SpecificGenerationIndex.Add(GenerationCount);
            GenerationIndex.Add(GenerationCount);
        }

        private void DrawTimeCharts()
        {
            if (TimeGraphResolution == -1)
            {
                TimeGraphChart.Series[0].Points.Clear();
                TimeGraphChart.Series[1].Points.Clear();
                try
                {
                    for (int i = LastStartIndex; i < GenerationIndex.Count; i++)
                    {
                        if (TimeChartRange == TimeChartRangeType.Last100)
                        {
                            if (GenerationIndex[i] < (GenerationCount - 100))
                            {
                                LastStartIndex = i;
                                continue;
                            }
                        }
                        if (TimeChartRange == TimeChartRangeType.Last500)
                        {
                            if (GenerationIndex[i] < (GenerationCount - 500))
                            {
                                LastStartIndex = i;
                                continue;
                            }
                        }
                        if (TimeChartRange == TimeChartRangeType.Last1000)
                        {
                            if (GenerationIndex[i] < (GenerationCount - 1000))
                            {
                                LastStartIndex = i;
                                continue;
                            }
                        }
                        AddTimeChartPoint(0, i);
                        AddTimeChartPoint(1, i);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    if (TimeGraphChart.Series[0].Points.Count >= TimeGraphResolution) TimeGraphChart.Series[0].Points.Clear();
                    if (TimeGraphChart.Series[1].Points.Count >= TimeGraphResolution) TimeGraphChart.Series[1].Points.Clear();
                    int IncrementAmount = ((GenerationIndex.Count - LastStartIndex) / TimeGraphResolution) + 1;
                    for (int i = LastStartIndex; i < GenerationIndex.Count; i += IncrementAmount)
                    {
                        if (TimeChartRange == TimeChartRangeType.Last100)
                        {
                            if (GenerationIndex[i] < (GenerationCount - 100))
                            {
                                LastStartIndex = i;
                                continue;
                            }
                        }
                        if (TimeChartRange == TimeChartRangeType.Last500)
                        {
                            if (GenerationIndex[i] < (GenerationCount - 500))
                            {
                                LastStartIndex = i;
                                continue;
                            }
                        }
                        if (TimeChartRange == TimeChartRangeType.Last1000)
                        {
                            if (GenerationIndex[i] < (GenerationCount - 1000))
                            {
                                LastStartIndex = i;
                                continue;
                            }
                        }
                        AddTimeChartPoint(0, i);
                        AddTimeChartPoint(1, i);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void AddTimeChartPoint(int Series, int Index)
        {
            if (Series == 0)
            {
                if (Chart1Type == TCDisplayedType.AverageAcceleration)
                {
                    TimeGraphChart.Series[0].Points.AddXY(GenerationIndex[Index], AverageAccelerations[Index]);
                }
                if (Chart1Type == TCDisplayedType.AverageVelocity)
                {
                    TimeGraphChart.Series[0].Points.AddXY(GenerationIndex[Index], AverageVelocities[Index]);
                }
                if (Chart1Type == TCDisplayedType.SpecificAcceleration)
                {
                    TimeGraphChart.Series[0].Points.AddXY(SpecificGenerationIndex[Index], SpecificAccelerations[Index]);
                }
                if (Chart1Type == TCDisplayedType.SpecificVelocity)
                {
                    TimeGraphChart.Series[0].Points.AddXY(SpecificGenerationIndex[Index], SpecificVelocities[Index]);
                }
            }
            else
            {
                if (Chart2Type == TCDisplayedType.AverageAcceleration)
                {
                    TimeGraphChart.Series[1].Points.AddXY(GenerationIndex[Index], AverageAccelerations[Index]);
                }
                if (Chart2Type == TCDisplayedType.AverageVelocity)
                {
                    TimeGraphChart.Series[1].Points.AddXY(GenerationIndex[Index], AverageVelocities[Index]);
                }
                if (Chart2Type == TCDisplayedType.SpecificAcceleration)
                {
                    TimeGraphChart.Series[1].Points.AddXY(SpecificGenerationIndex[Index], SpecificAccelerations[Index]);
                }
                if (Chart2Type == TCDisplayedType.SpecificVelocity)
                {
                    TimeGraphChart.Series[1].Points.AddXY(SpecificGenerationIndex[Index], SpecificVelocities[Index]);
                }
            }
        }

        private void UI_1_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 1;
        }

        private void UI_50_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 50;
        }

        private void UI_100_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 100;
        }

        private void UI_200_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 200;
        }

        private void UI_500_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 500;
        }

        private void UI_1000_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 1000;
        }

        private void UI_2000_Click(object sender, EventArgs e)
        {
            SimTimer.Interval = 2000;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            ResumeButton.Enabled = true;
            StopButton.Enabled = false;
            SimTimer.Stop();
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void SetScale(double Scale)
        {
            if (Scale != Double.NaN) UniverseScale = Scale;
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -Scale;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = Scale;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -Scale;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = Scale;
        }

        private void PrimaryGrid_MouseClick(object sender, MouseEventArgs e)
        {
            Particle NewParticle = new Particle(2, 1);
            NewParticle.Properties[0] = DropWeight;

            NewParticle.Position[0] = PrimaryGrid.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
            NewParticle.Position[1] = PrimaryGrid.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

            ParticleList.Capacity++;
            ParticleList.Add(NewParticle);
        }

        public void PrintRawData(Particle Input, int Index)
        {
            string[] DataString = { Index.ToString(), Input.Properties[0].ToString("n1"), 
                                      Input.Acceleration[0].ToString("n6") + "          " + Input.Acceleration[1].ToString("n6"),
                                      Input.Velocity[0].ToString("n6") + "          " + Input.Velocity[1].ToString("n6"),
                                      Input.Position[0].ToString("n4") + "          " + Input.Position[1].ToString("n4") };
            RawData.Items.Add(new ListViewItem(DataString));
        }

        private void particlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(50, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(50, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void crazyMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(10, 100000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void blackHoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
            }

            ParticleList[0].Properties[0] = 10000000;

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void uniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = 1000;
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void singularityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 10, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 10, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void antiGravToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.RandomGen.Next(-1, 2) * Simulation.GenerateRandomDouble(100, 100000);
                if (ParticleList[i].Properties[0] == 0) ParticleList[i].Properties[0] = 1;
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void bH300ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(300, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(300, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
            }

            ParticleList[0].Properties[0] = 10000000;

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void cM300ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(300, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(300, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(10, 100000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        public void UpdateAdditionalInfo()
        {
            string NL = Environment.NewLine;
            InfoBox.Text = "";
            InfoBox.Text += "Number of Particles:     " + ParticleList.Count + NL;
            int Calcs = (ParticleList.Count * ParticleList.Count) * 2;
            if (Collisions == true) Calcs += (ParticleList.Count * ParticleList.Count) * 2;
            InfoBox.Text += "Calculations/Generation: " + Calcs.ToString() + NL;
            if (ParallelProcessing)
            {
                InfoBox.Text += "Parallel Processing:     ON" + NL;
            }
            else
            {
                InfoBox.Text += "Parallel Processing:     OFF" + NL;
            }
            if (RenderOnlyVisible)
            {
                InfoBox.Text += "Selective Rendering:     ONLY VISIBLE" + NL;
            }
            else
            {
                if (SelectiveRenderingOn)
                {
                    InfoBox.Text += "Selective Rendering:     SELECTIVE" + NL;
                    InfoBox.Text += "Number Plotted:          " + PrimaryGrid.Series[0].Points.Count + NL;
                }
                else
                {
                    InfoBox.Text += "Selective Rendering:     OFF" + NL;
                }
            }
            if (RenderHQ.Enabled == true)
            {
                InfoBox.Text += "Render Quality:          LOW" + NL;
            }
            else
            {
                InfoBox.Text += "Render Quality:          HIGH" + NL;
            }
            InfoBox.Text += "Gravity Constant:        " + GravityConstant.ToString() + " N(m/kg)^2" + NL;
            InfoBox.Text += "Precision:               " + Precision.ToString() + " m" + NL;
            if (Collisions)
            {
                InfoBox.Text += "Collisions:              ON" + NL;
                InfoBox.Text += "Collision Mode:          " + CollisionType.ToString().ToUpper() + NL;
                InfoBox.Text += "Collision Precision:     " + (Precision / CollisionsDivider).ToString() + " m" + NL;
            }
            else
            {
                InfoBox.Text += "Collisions:              OFF" + NL;
            }
            InfoBox.Text += "Softening Value:         " + SofteningValue.ToString() + NL;
            InfoBox.Text += "Max Component Velocity:  " + MaxVelocity.ToString() + " m/s" + NL;
            InfoBox.Text += "Total Mass:              " + Simulation.CalculateTotalMass(ParticleList).ToString("n4") + " kg" + NL;
            InfoBox.Text += "Average Mass:            " + Simulation.CalculateAverageMass(ParticleList).ToString("n4") + " kg" + NL + NL;
            List<double> TEMP = Simulation.CalculateCenterOfPosition(ParticleList);
            InfoBox.Text += "Center of Position (m):       " + NL + TEMP[0].ToString("n4") + "\t" + TEMP[1].ToString("n4") + NL + NL;
            TEMP = Simulation.CalculateAverageAcceleration(ParticleList);
            InfoBox.Text += "Average Acceleration (m/s^2): " + NL + TEMP[0].ToString("n6") + "\t" + TEMP[1].ToString("n6") + NL + NL;
            TEMP = Simulation.CalculateAverageVelocity(ParticleList);
            InfoBox.Text += "Average Velocity (m/s):       " + NL + TEMP[0].ToString("n6") + "\t" + TEMP[1].ToString("n6") + NL + NL;
        }

        private void orbitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            ParticleList = Simulation.Get_RandomParticleSet(5, 100, 2, 1);

            ParticleList[0].Properties[0] = 1000000;
            ParticleList[0].Position[0] = 0;
            ParticleList[0].Position[1] = 0;
            ParticleList[1].Properties[0] = 1;
            ParticleList[1].Velocity[1] = 0.4;
            ParticleList[1].Position[0] = 100;
            ParticleList[1].Position[1] = 0;
            ParticleList[2].Properties[0] = 1;
            ParticleList[2].Velocity[1] = -0.6;
            ParticleList[2].Position[0] = -50;
            ParticleList[2].Position[1] = 0;
            ParticleList[3].Properties[0] = 1;
            ParticleList[3].Velocity[0] = -0.3;
            ParticleList[3].Position[0] = 0;
            ParticleList[3].Position[1] = 150;
            ParticleList[4].Properties[0] = 1;
            ParticleList[4].Velocity[0] = -0.2;
            ParticleList[4].Position[0] = 0;
            ParticleList[4].Position[1] = -200;

            //Set the generation count to zero
            GenerationCount = 0;
            
            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void bangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 1, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 1, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = -Simulation.GenerateRandomDouble(10, 100000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void OpenSim_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            OFD.ShowDialog();
        }

        private void SaveSim_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            SFD.ShowDialog();
        }

        private void OFD_FileOk(object sender, CancelEventArgs e)
        {
            ParticleList = Serialization.DeserializeParticleList(OFD.FileName);

            PrimaryGrid.Series[0].Points.Clear();

            //For each particle in the list:
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                PrimaryGrid.Series[0].Points.AddXY(ParticleList[i].Position[0], ParticleList[i].Position[1]);
            }

            GenerationCount = 0;
            ResumeButton.Enabled = true;
        }

        private void SFD_FileOk(object sender, CancelEventArgs e)
        {
            Serialization.SerializeParticleList(SFD.FileName, ParticleList);
            MessageBox.Show("Simulation successfully saved...", "Save Successful!", MessageBoxButtons.OK);
        }

        private void sG300ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(300, 10, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(300, 10, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void e6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E6;
        }

        private void e0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY;
        }

        private void e4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E4;
        }

        private void e8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E8;
        }

        private void e10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E10;
        }

        private void e12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E12;
        }

        private void e14ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E14;
        }

        private void e7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E7;
        }

        private void e9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityConstant = GRAVITY * 1.0E9;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MaxVelocity = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MaxVelocity = 10;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MaxVelocity = 50;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            MaxVelocity = 100;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            MaxVelocity = 1000;
        }

        private void nONEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaxVelocity = 1000000;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            SofteningValue = 0;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            SofteningValue = 2;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            SofteningValue = 5;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            SofteningValue = 10;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            SofteningValue = 20;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Precision = 0.0001;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Precision = 0.01;
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            Precision = 0.1;
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Precision = 1;
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Precision = 10;
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            Precision = 100;
        }

        private void Button_ResetDefaults_Click(object sender, EventArgs e)
        {
            Precision = 0.01;
            SofteningValue = 5;
            MaxVelocity = 100;
            GravityConstant = GRAVITY * 1.0E8;
            CollisionsDivider = 2;
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            MaxVelocity = 500;
        }

        private void Button_NEGATE_Click(object sender, EventArgs e)
        {
            GravityConstant = GravityConstant * -1;
        }

        private void aBSOLUTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Precision = 0;
        }

        private void randomVelocityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            //First, we need to create a new list of particles
            if (ThisSeedType == SeedType.Rectangular) ParticleList = Simulation.Get_RandomParticleSet(200, 100, 2, 1);
            if (ThisSeedType == SeedType.Polar) ParticleList = Simulation.Get_RandomCircularParticleSet(200, 100, 1);

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
                for (int j = 0; j < ParticleList[i].Velocity.Capacity; j++)
                {
                    ParticleList[i].Velocity[j] = Simulation.RandomGen.Next(-1, 2) * Simulation.GenerateRandomDouble(0.1, 0.25);
                }
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Start the timer
            ResumeButton.Enabled = false;
            StopButton.Enabled = true;
            SimTimer.Start();
        }

        private void Collisions_ON_Click(object sender, EventArgs e)
        {
            Collisions_ON.Enabled = false;
            Collisions_OFF.Enabled = true;
            Collisions = true;
        }

        private void Collisions_OFF_Click(object sender, EventArgs e)
        {
            Collisions_ON.Enabled = true;
            Collisions_OFF.Enabled = false;
            Collisions = false;
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            MaxVelocity = 5;
        }

        private void halfPrecisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollisionsDivider = 2;
        }

        private void fullPrecisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollisionsDivider = 1;
        }

        private void quarterPrecisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollisionsDivider = 4;
        }

        private void thirdPrecisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollisionsDivider = 3;
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            DropWeight = 1;
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            DropWeight = 10;
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            DropWeight = 100;
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            DropWeight = 1000;
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            DropWeight = 10000;
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            DropWeight = 10000000;
        }

        private void ReCenter_Click(object sender, EventArgs e)
        {
            if (SimTimer.Enabled)
            {
                SimTimer.Stop();
                ParticleList = Simulation.ReCenterSimulation(ParticleList);
                SimTimer.Start();
            }
            else
            {
                ParticleList = Simulation.ReCenterSimulation(ParticleList);
            }
        }

        private void BufferCurrent_Click(object sender, EventArgs e)
        {
            Buffering x = new Buffering(ParticleList, Precision, SofteningValue, MaxVelocity, GravityConstant, Collisions, CollisionsDivider, CollisionType, BoundaryType, UniverseScale);
            x.ShowDialog();
        }

        private void OBFD_FileOk(object sender, CancelEventArgs e)
        {
            LoadSaveBSim x = new LoadSaveBSim(OBFD.FileName);
            x.ShowDialog();
            /*BufferedSimulation Sim = Serialization.DeserializeBSim(OBFD.FileName);
            BufferedViewer x = new BufferedViewer(Sim);
            x.Show();*/
        }

        private void openBufferedSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (SimTimer.Enabled) SimTimer.Stop();

            OBFD.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ParallelON_Click(object sender, EventArgs e)
        {
            ParallelProcessing = true;
            ParallelON.Enabled = false;
            ParallelOFF.Enabled = true;
        }

        private void ParallelOFF_Click(object sender, EventArgs e)
        {
            ParallelProcessing = false;
            ParallelON.Enabled = true;
            ParallelOFF.Enabled = false;
        }

        private void RendAll_Click(object sender, EventArgs e)
        {
            RendAll.Enabled = false;
            RendOnlyVisible.Enabled = true;
            SelectiveRendering.Enabled = true;
            SelectiveRenderingOn = false;
            RenderOnlyVisible = false;
        }

        private void RendOnlyVisible_Click(object sender, EventArgs e)
        {
            RendAll.Enabled = true;
            RendOnlyVisible.Enabled = false;
            SelectiveRendering.Enabled = true;
            SelectiveRenderingOn = false;
            RenderOnlyVisible = true;
        }

        private bool IsInView(List<double> Position)
        {
            if (Position[0] < PrimaryGrid.ChartAreas[0].AxisX.Minimum || Position[0] > PrimaryGrid.ChartAreas[0].AxisX.Maximum) return false;
            if (Position[1] < PrimaryGrid.ChartAreas[0].AxisY.Minimum || Position[1] > PrimaryGrid.ChartAreas[0].AxisY.Maximum) return false;

            return true;
        }

        private void RenderHQ_Click(object sender, EventArgs e)
        {
            RenderHQ.Enabled = false;
            PrimaryGrid.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.All;
            SelectiveRenderResolution = 400;
            RenderLQ.Enabled = true;
        }

        private void RenderLQ_Click(object sender, EventArgs e)
        {
            RenderHQ.Enabled = true;
            PrimaryGrid.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.None;
            SelectiveRenderResolution = 250;
            RenderLQ.Enabled = false;
        }

        private void SelectiveRendering_Click(object sender, EventArgs e)
        {
            RendAll.Enabled = true;
            RendOnlyVisible.Enabled = true;
            SelectiveRendering.Enabled = false;
            RenderOnlyVisible = false;
            SelectiveRenderingOn = true;
        }

        private void RectangularSeed_Click(object sender, EventArgs e)
        {
            RectangularSeed.Enabled = false;
            PolarSeed.Enabled = true;
            ThisSeedType = SeedType.Rectangular;
        }

        private void PolarSeed_Click(object sender, EventArgs e)
        {
            RectangularSeed.Enabled = true;
            PolarSeed.Enabled = false;
            ThisSeedType = SeedType.Polar;
        }

        private void Menu_Fun_Click(object sender, EventArgs e)
        {

        }

        private void Seed_10000_Click(object sender, EventArgs e)
        {
            SeedParticles(10000, 400);
        }

        private void Seed_50_Click(object sender, EventArgs e)
        {
            SeedParticles(50, 100);
        }

        private void Seed_200_Click(object sender, EventArgs e)
        {
            SeedParticles(200, 100);
        }

        private void Seed_300_Click(object sender, EventArgs e)
        {
            SeedParticles(300, 100);
        }

        private void Seed_500_Click(object sender, EventArgs e)
        {
            SeedParticles(500, 100);
        }

        private void Seed_1000_Click(object sender, EventArgs e)
        {
            SeedParticles(1000, 200);
        }

        private void Seed_5000_Click(object sender, EventArgs e)
        {
            SeedParticles(5000, 300);
        }

        private void Seed_7500_Click(object sender, EventArgs e)
        {
            SeedParticles(7500, 350);
        }

        private void SeedSettings_RegularMass_Click(object sender, EventArgs e)
        {
            IsCrazyMassSeed = false;
            SeedSettings_RegularMass.Enabled = false;
            SeedSettings_CrazyMass.Enabled = true;
        }

        private void SeedSettings_CrazyMass_Click(object sender, EventArgs e)
        {
            IsCrazyMassSeed = true;
            SeedSettings_RegularMass.Enabled = true;
            SeedSettings_CrazyMass.Enabled = false;
        }

        private void Seed_20000_Click(object sender, EventArgs e)
        {
            SeedParticles(20000, 500);
        }

        private void CollisionMode_PIE_Click(object sender, EventArgs e)
        {
            CollisionMode_PIE.Enabled = false;
            CollisionMode_PE.Enabled = true;
            CollisionType = CollisionMode.PerfectlyInelastic;
        }

        private void CollisionMode_PE_Click(object sender, EventArgs e)
        {
            CollisionMode_PIE.Enabled = true;
            CollisionMode_PE.Enabled = false;
            CollisionType = CollisionMode.PerfectlyElastic;
        }

        private void CDT_Line_Click(object sender, EventArgs e)
        {
            TimeGraphChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            TimeGraphChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
        }

        private void CDT_SLine_Click(object sender, EventArgs e)
        {
            TimeGraphChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            TimeGraphChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void CDT_Column_Click(object sender, EventArgs e)
        {
            TimeGraphChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            TimeGraphChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void TimeGraphChart_Click(object sender, EventArgs e)
        {

        }

        private void TimeGraphs_ON_Click(object sender, EventArgs e)
        {
            bool wasenabled = SimTimer.Enabled;
            if (wasenabled) SimTimer.Stop();
            TimeGraphs_ON.Enabled = false;
            TimeGraphs_OFF.Enabled = true;
            TGMenu_DataRange.Visible = true;
            TGMenu_DisplayType.Visible = true;
            TGMenu_Resolution.Visible = true;
            TGMenu_Charts.Visible = true;
            FakeSeperator.Visible = true;
            WatchIndexLabel.Visible = true;
            WatchIndexBox.Visible = true;
            TimeGraphsOn = true;
            AverageAccelerations = new List<double>();
            AverageVelocities = new List<double>();
            GenerationIndex = new List<int>();
            SpecificGenerationIndex = new List<int>();
            SpecificVelocities = new List<double>();
            SpecificAccelerations = new List<double>();
            LastStartIndex = 0;
            if (wasenabled) SimTimer.Start();
        }

        private void TimeGraphs_OFF_Click(object sender, EventArgs e)
        {
            bool wasenabled = SimTimer.Enabled;
            if (wasenabled) SimTimer.Stop();
            TimeGraphs_ON.Enabled = true;
            TimeGraphs_OFF.Enabled = false;
            TGMenu_DataRange.Visible = false;
            TGMenu_DisplayType.Visible = false;
            TGMenu_Resolution.Visible = false;
            TGMenu_Charts.Visible = false;
            FakeSeperator.Visible = false;
            WatchIndexLabel.Visible = false;
            WatchIndexBox.Visible = false;
            TimeGraphsOn = false;
            if (wasenabled) SimTimer.Start();
        }

        private void CDR_AllGenerations_Click(object sender, EventArgs e)
        {
            TimeChartRange = TimeChartRangeType.All;
            LastStartIndex = 0;
        }

        private void CDR_Last100_Click(object sender, EventArgs e)
        {
            TimeChartRange = TimeChartRangeType.Last100;
            LastStartIndex = 0;
        }

        private void CDR_Last500_Click(object sender, EventArgs e)
        {
            TimeChartRange = TimeChartRangeType.Last500;
            LastStartIndex = 0;
        }

        private void MainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            TimeGraphResolution = 50;
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            TimeGraphResolution = 100;
        }

        private void absoluteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TimeGraphResolution = -1;
        }

        private void CDR_Last1000_Click(object sender, EventArgs e)
        {
            TimeChartRange = TimeChartRangeType.Last1000;
            LastStartIndex = 0;
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            TimeGraphResolution = 10;
        }

        private void toolStripMenuItem29_Click_1(object sender, EventArgs e)
        {
            TimeGraphResolution = 500;
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            TimeGraphResolution = 1000;
        }

        private void averageAccelerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chart1Type = TCDisplayedType.AverageAcceleration;
            TimeGraphChart.ChartAreas[0].AxisY.Title = "Avg. Acceleration";
            TimeGraphChart.Series[0].Points.Clear();
        }

        private void Chart1_AverageVel_Click(object sender, EventArgs e)
        {
            Chart1Type = TCDisplayedType.AverageVelocity;
            TimeGraphChart.ChartAreas[0].AxisY.Title = "Avg. Velocity";
            TimeGraphChart.Series[0].Points.Clear();
        }

        private void Chart2_AverageAccel_Click(object sender, EventArgs e)
        {
            Chart2Type = TCDisplayedType.AverageAcceleration;
            TimeGraphChart.ChartAreas[1].AxisY.Title = "Avg. Acceleration";
            TimeGraphChart.Series[1].Points.Clear();
        }

        private void Chart2_AverageVel_Click(object sender, EventArgs e)
        {
            Chart2Type = TCDisplayedType.AverageVelocity;
            TimeGraphChart.ChartAreas[1].AxisY.Title = "Avg. Velocity";
            TimeGraphChart.Series[1].Points.Clear();
        }

        private void WatchIndexBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                try
                {
                    int Temp = Convert.ToInt32(WatchIndexBox.Text);
                    if (Temp >= 0 && Temp < ParticleList.Count)
                    {
                        AverageAccelerations = new List<double>();
                        AverageVelocities = new List<double>();
                        GenerationIndex = new List<int>();
                        SpecificGenerationIndex = new List<int>();
                        SpecificVelocities = new List<double>();
                        SpecificAccelerations = new List<double>();
                        LastStartIndex = 0;
                        SpecificIndex = Temp;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void Chart1_SpecificAccel_Click(object sender, EventArgs e)
        {
            Chart1Type = TCDisplayedType.SpecificAcceleration;
            TimeGraphChart.ChartAreas[0].AxisY.Title = "Specific Acceleration";
            TimeGraphChart.Series[0].Points.Clear();
            //SpecificGenerationIndex = new List<int>();
            //SpecificVelocities = new List<double>();
            //SpecificAccelerations = new List<double>();
        }

        private void Chart1_SpecificVel_Click(object sender, EventArgs e)
        {
            Chart1Type = TCDisplayedType.SpecificVelocity;
            TimeGraphChart.ChartAreas[0].AxisY.Title = "Specific Velocity";
            TimeGraphChart.Series[0].Points.Clear();
            //SpecificGenerationIndex = new List<int>();
            //SpecificVelocities = new List<double>();
            //SpecificAccelerations = new List<double>();
        }

        private void Chart2_SpecificAccel_Click(object sender, EventArgs e)
        {
            Chart2Type = TCDisplayedType.SpecificAcceleration;
            TimeGraphChart.ChartAreas[1].AxisY.Title = "Specific Acceleration";
            TimeGraphChart.Series[1].Points.Clear();
            //SpecificGenerationIndex = new List<int>();
            //SpecificVelocities = new List<double>();
            //SpecificAccelerations = new List<double>();
        }

        private void Chart2_SpecificVel_Click(object sender, EventArgs e)
        {
            Chart2Type = TCDisplayedType.SpecificVelocity;
            TimeGraphChart.ChartAreas[1].AxisY.Title = "Specific Velocity";
            TimeGraphChart.Series[1].Points.Clear();
            //SpecificGenerationIndex = new List<int>();
            //SpecificVelocities = new List<double>();
            //SpecificAccelerations = new List<double>();
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            SofteningValue = 0.5;
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            SofteningValue = 100;
        }

        private void Scale_Fit_Click(object sender, EventArgs e)
        {
            SetScale(Double.NaN);
        }

        private void Scale_50_Click(object sender, EventArgs e)
        {
            SetScale(50);
        }

        private void Scale_100_Click(object sender, EventArgs e)
        {
            SetScale(100);
        }

        private void Scale_500_Click(object sender, EventArgs e)
        {
            SetScale(500);
        }

        private void Scale_1000_Click(object sender, EventArgs e)
        {
            SetScale(1000);
        }

        private void Scale_5000_Click(object sender, EventArgs e)
        {
            SetScale(5000);
        }

        private void Scale_10000_Click(object sender, EventArgs e)
        {
            SetScale(10000);
        }

        private void Scale_50000_Click(object sender, EventArgs e)
        {
            SetScale(50000);
        }

        private void ManipulateTrackBar_Scroll(object sender, EventArgs e)
        {
            if (!TrackBarChanging) UpdateManipulatedVariables();
        }

        private void UpdateManipulatedVariables()
        {
            bool wasrunning = SimTimer.Enabled;
            if (wasrunning) SimTimer.Stop();
            switch (ManipulateBox.SelectedItem.ToString().ToLower())
            {
                case "gravitational constant":
                    UpdateGravityConstant();
                    break;
                case "calculation precision":
                    UpdatePrecision();
                    break;
                case "collision precision":
                    UpdateCollisionPrecision();
                    break;
                case "maximum velocity":
                    UpdateMaxVel();
                    break;
                case "softening value":
                    UpdateSofteningValue();
                    break;
                default:
                    break;
            }
            if (wasrunning) SimTimer.Start();
        }

        private void UpdateGravityConstant()
        {
            //Update the text box
            double NonScientific = (double)ManipulateTrackBar.Value / 100.0;
            ManipulateTB.Text = (NonScientific).ToString("n2") + "  X";

            //Update Gravity
            switch (ManipulateExponent.SelectedItem.ToString())
            {
                case "100":
                    GravityConstant = NonScientific * 100;
                    break;
                case "10":
                    GravityConstant = NonScientific * 10;
                    break;
                case "1":
                    GravityConstant = NonScientific;
                    break;
                case "0.1":
                    GravityConstant = NonScientific * 0.1;
                    break;
                case "0.01":
                    GravityConstant = NonScientific * 0.01;
                    break;
                case "10^-3 (Default)":
                    GravityConstant = NonScientific * 1.0E-3;
                    break;
                case "10^-4":
                    GravityConstant = NonScientific * 1.0E-4;
                    break;
                case "10^-5":
                    GravityConstant = NonScientific * 1.0E-5;
                    break;
                case "10^-7":
                    GravityConstant = NonScientific * 1.0E-7;
                    break;
                case "10^-9":
                    GravityConstant = NonScientific * 1.0E-9;
                    break;
                case "10^-11 (Natural)":
                    GravityConstant = NonScientific * 1.0E-11;
                    break;

                default: //If we can't figure it out, assume natural
                    GravityConstant = GRAVITY;
                    break;
            }
        }

        private void UpdateCollisionPrecision()
        {
            //Update the text box
            double NonScientific = (double)ManipulateTrackBar.Value / 100.0;
            ManipulateTB.Text = (NonScientific).ToString("n2");

            CollisionsDivider = NonScientific;
        }

        private void UpdatePrecision()
        {
            //Update the text box
            double NonScientific = (double)ManipulateTrackBar.Value / 1000.0;
            ManipulateTB.Text = (NonScientific).ToString("n3");

            Precision = NonScientific;
        }

        private void UpdateMaxVel()
        {
            //Update the text box
            double NonScientific = (double)ManipulateTrackBar.Value;
            ManipulateTB.Text = (NonScientific).ToString();

            MaxVelocity = NonScientific;
        }

        private void UpdateSofteningValue()
        {
            //Update the text box
            double NonScientific = (double)ManipulateTrackBar.Value / 10.0;
            ManipulateTB.Text = (NonScientific).ToString("n1");

            SofteningValue = NonScientific;
        }

        private void ManipulateExponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TrackBarChanging) UpdateManipulatedVariables();
        }

        private void ManipulateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TrackBarChanging) Manipulation_SetTrackBar();
        }

        private void BoundaryType_Infinate_Click(object sender, EventArgs e)
        {
            BoundaryType = UniverseBoundaryType.Infinate;
        }

        private void BoundaryType_Fixed_Click(object sender, EventArgs e)
        {
            BoundaryType = UniverseBoundaryType.Fixed_HardEdge;
        }

        private void BoundaryType_Fixed_Wrapped_Click(object sender, EventArgs e)
        {
            BoundaryType = UniverseBoundaryType.Fixed_Wrapped;
        }

        private void BoundaryType_Fixed_Relative_Click(object sender, EventArgs e)
        {
            BoundaryType = UniverseBoundaryType.Fixed_Relative;
        }

        private void Start3DTest_Click(object sender, EventArgs e)
        {
            //Stop the timer if enabled
            if (Timer3D.Enabled) Timer3D.Stop();

            //First, we need to create a new list of particles
            ParticleList = Simulation.Get_RandomParticleSet(300, 200, 3, 1);

            //Prepare the ILNumerics Grid

            //Now lets set the mass of these particles to a random number
            for (int i = 0; i < ParticleList.Capacity; i++)
            {
                if (!IsCrazyMassSeed) ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(1000, 10000);
                if (IsCrazyMassSeed) ParticleList[i].Properties[0] = Simulation.GenerateRandomDouble(10, 100000);

                //Also add the position to the ILNumerics scene
                //points.Positions.Update(Simulation.ConvertDoubleListToFloatList(ParticleList[i].Position).ToArray());
            }

            //Set the generation count to zero
            GenerationCount = 0;

            //Add the scene to the panel
            //Grid3D.

            //Start the timer
            Timer3D.Start();
        }

        private void Stop3DTest_Click(object sender, EventArgs e)
        {
            Timer3D.Stop();
        }
    }
}
