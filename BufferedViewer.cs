using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace UniverseSimulator
{
    public partial class BufferedViewer : Form
    {
        //Variables
        public int GenerationCount = 0;
        public BufferedSimulation Sim;

        //Render Settings
        public static bool RenderVisible = false;
        public static bool Loop = false;
        public static bool SelectiveRenderingOn = false;
        public static int SelectiveRenderResolution = 500;

        public BufferedViewer(BufferedSimulation Buffered)
        {
            InitializeComponent();
            Sim = Buffered;
            TimeTracker.Maximum = Sim.Data.Count - 1;
            TimeTracker.TickFrequency = Sim.Data.Count / 20;
        }

        private void BufferedViewer_Load(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            this.Activate();
        }

        private void rESETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeTracker.Value = 0;
        }

        private void PLAY_Click(object sender, EventArgs e)
        {
            //if we are past the end of the simulation
            if (GenerationCount >= Sim.Data.Count)
            {
                GenerationCount = 0;
            }

            Playback.Start();
        }

        private void STOP_Click(object sender, EventArgs e)
        {
            Playback.Stop();
        }

        private void Playback_Tick(object sender, EventArgs e)
        {
            Playback.Stop();

            //if we are past the end of the simulation
            if (GenerationCount >= Sim.Data.Count)
            {
                if (Loop)
                {
                    GenerationCount = 0;
                }
                else
                {
                    return;
                }
            }

            
            TimeTracker.Value = GenerationCount;
            PrimaryGrid.Titles[0].Text = "Generation " + GenerationCount;

            //If we are selective rendering
            if (SelectiveRenderingOn)
            {
                SelectiveRender();
            }
            else
            {
                PrimaryGrid.Series[0].Points.Clear();
                for (int i = 0; i < Sim.Data[GenerationCount].Capacity; i++)
                {
                    if (RenderVisible == false)
                    {
                        PrimaryGrid.Series[0].Points.AddXY(Sim.Data[GenerationCount][i][0], Sim.Data[GenerationCount][i][1]);
                    }
                    else
                    {
                        if (IsInView(Sim.Data[GenerationCount][i]))
                        {
                            PrimaryGrid.Series[0].Points.AddXY(Sim.Data[GenerationCount][i][0], Sim.Data[GenerationCount][i][1]);
                        }
                    }
                }
            }

            GenerationCount++;
            Playback.Start();
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
            for (int i = 0; i < Sim.Data[GenerationCount].Capacity; i++)
            {
                //Determine what slot it goes in
                int LocX = (int)Math.Round((Sim.Data[GenerationCount][i][0] - PrimaryGrid.ChartAreas[0].AxisX.Minimum) / SlotSizeX);
                int LocY = (int)Math.Round((Sim.Data[GenerationCount][i][1] - PrimaryGrid.ChartAreas[0].AxisY.Minimum) / SlotSizeY);

                //Determine if we are off the grid
                if (LocX < 0 || LocX >= SelectiveRenderResolution) continue;
                if (LocY < 0 || LocY >= SelectiveRenderResolution) continue;

                //If the position isn't taken:
                if (IsTaken[LocX, LocY] == false)
                {
                    //Make it taken and draw the point
                    IsTaken[LocX, LocY] = true;
                    PrimaryGrid.Series[0].Points.AddXY(Sim.Data[GenerationCount][i][0], Sim.Data[GenerationCount][i][1]);
                }
                else
                {
                    continue;
                }
            }

        }

        private bool IsInView(List<double> Position)
        {
            if (Position[0] < PrimaryGrid.ChartAreas[0].AxisX.Minimum || Position[0] > PrimaryGrid.ChartAreas[0].AxisX.Maximum) return false;
            if (Position[1] < PrimaryGrid.ChartAreas[0].AxisY.Minimum || Position[1] > PrimaryGrid.ChartAreas[0].AxisY.Maximum) return false;

            return true;
        }

        private void TimeTracker_Scroll(object sender, EventArgs e)
        {

        }

        private void TimeTracker_ValueChanged(object sender, EventArgs e)
        {
            GenerationCount = TimeTracker.Value;
            PrimaryGrid.Series[0].Points.Clear();
            TimeTracker.Value = GenerationCount;
            PrimaryGrid.Titles[0].Text = "Generation " + GenerationCount;

            for (int i = 0; i < Sim.Data[GenerationCount].Capacity; i++)
            {
                PrimaryGrid.Series[0].Points.AddXY(Sim.Data[GenerationCount][i][0], Sim.Data[GenerationCount][i][1]);
            }
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Playback.Enabled) Playback.Stop();
            SFD.ShowDialog();
        }

        private void SFD_FileOk(object sender, CancelEventArgs e)
        {
            //Serialization.SerializeBSim(SFD.FileName, Sim);
            //MessageBox.Show("Successfully saved buffered simulation to file...", "Save Successful!", MessageBoxButtons.OK);
            LoadSaveBSim x = new LoadSaveBSim(SFD.FileName, this.Sim);
            x.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -50;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 50;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -50;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 50;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -100;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 100;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -100;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 100;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -500;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 500;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -500;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 500;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -1000;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 1000;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -1000;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 1000;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -5000;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 5000;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -5000;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 5000;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -10000;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 10000;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -10000;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 10000;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = -50000;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = 50000;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = -50000;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = 50000;
        }

        private void fitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrimaryGrid.ChartAreas[0].AxisX.Minimum = Double.NaN;
            PrimaryGrid.ChartAreas[0].AxisX.Maximum = Double.NaN;
            PrimaryGrid.ChartAreas[0].AxisY.Minimum = Double.NaN;
            PrimaryGrid.ChartAreas[0].AxisY.Maximum = Double.NaN;
        }

        private void RenderAll_Click(object sender, EventArgs e)
        {
            RenderVisible = false;
            RenderAll.Enabled = false;
            SelectiveRendering.Enabled = true;
            SelectiveRenderingOn = false;
            RenderOnlyVisible.Enabled = true;
        }

        private void RenderOnlyVisible_Click(object sender, EventArgs e)
        {
            RenderVisible = true;
            RenderAll.Enabled = true;
            SelectiveRendering.Enabled = true;
            SelectiveRenderingOn = false;
            RenderOnlyVisible.Enabled = false;
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

        private void Loop_On_Click(object sender, EventArgs e)
        {
            Loop_On.Enabled = false;
            Loop = true;
            Loop_Off.Enabled = true;
        }

        private void Loop_Off_Click(object sender, EventArgs e)
        {
            Loop_On.Enabled = true;
            Loop = false;
            Loop_Off.Enabled = false;
        }

        private void SelectiveRendering_Click(object sender, EventArgs e)
        {
            RenderVisible = false;
            RenderAll.Enabled = true;
            SelectiveRendering.Enabled = false;
            SelectiveRenderingOn = true;
            RenderOnlyVisible.Enabled = true;
        }
    }
}
