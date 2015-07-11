using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UniverseSimulator
{
    public partial class LoadSaveBSim : Form
    {
        public bool IsSaving;
        public BufferedSimulation WorkingSim;
        public string FilePath;

        /// <param name="FileName">The file path to open the buffered simulation from</param>
        public LoadSaveBSim(string FileName)
        {
            
            InitializeComponent();
            this.IsSaving = false;
            this.FilePath = FileName;
        }

        /// <param name="FileName">The file path to save the buffered simulation to</param>
        /// <param name="SimToSave">The buffered simulation to save</param>
        public LoadSaveBSim(string FileName, BufferedSimulation SimToSave)
        {
            
            InitializeComponent();
            this.IsSaving = true;
            this.WorkingSim = SimToSave;
            this.FilePath = FileName;
        }

        private void LoadSaveBSim_Load(object sender, EventArgs e)
        {
            if (this.IsSaving)
            {
                this.Text = "Saving Buffered Simulation...";
                Delay.Start();
            }
            else
            {
                this.Text = "Opening Buffered Simulation...";
                Delay.Start();
            }
        }

        private void SaveSim()
        {
            //Open the file and create a new binary formatter
            UpdateStatus("Saving to " + this.FilePath + "...");
            PBar.Style = ProgressBarStyle.Marquee;
            Stream stream = File.Open(this.FilePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();

            //Serialize the number of generations
            bFormatter.Serialize(stream, WorkingSim.Data.Capacity);

            //Prepare the progress bar
            PBar.Style = ProgressBarStyle.Blocks;
            PBar.Maximum = WorkingSim.Data.Capacity;
            PBar.Minimum = 0;

            //Write the data
            for (int i = 0; i < WorkingSim.Data.Capacity; i++)
            {
                UpdateStatus("Saving generation " + i + " of " + WorkingSim.Data.Capacity + "...");
                bFormatter.Serialize(stream, WorkingSim.Data[i]);
                PBar.Value++;
                double percent = ((double)i / (double)WorkingSim.Data.Capacity) * 100;
                this.Text = "Saving Buffered Simulation (" + percent.ToString("n2") + "% Complete)...";
            }

            //Close the stream
            stream.Close();

            MessageBox.Show("Successfully saved buffered simulation to file...", "Save Successful!", MessageBoxButtons.OK);
            Thread.Sleep(100);
            this.Close();
        }

        private void LoadSim()
        {
            //Open the file and create a new binary formatter
            UpdateStatus("Accessing " + this.FilePath + "...");
            PBar.Style = ProgressBarStyle.Marquee;
            Stream stream = File.Open(this.FilePath, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();

            //Deserialize the number of generations
            int NumGenerations = (int)bFormatter.Deserialize(stream);
            WorkingSim = new BufferedSimulation(NumGenerations);
            List<List<List<double>>> ReadData = new List<List<List<double>>>(NumGenerations);

            //Prepare the progress bar
            PBar.Style = ProgressBarStyle.Blocks;
            PBar.Maximum = NumGenerations;
            PBar.Minimum = 0;

            //Read the data
            for (int i = 0; i < NumGenerations; i++)
            {
                UpdateStatus("Loading generation " + i + " of " + NumGenerations + "...");
                ReadData.Add((List<List<double>>)bFormatter.Deserialize(stream));
                PBar.Value++;
                double percent = ((double)i / (double)NumGenerations) * 100;
                this.Text = "Opening Buffered Simulation (" + percent.ToString("n2") + "% Complete)...";
            }

            //Set the working sim
            WorkingSim.Data = ReadData;

            //Close the stream
            stream.Close();

            //Open the Buffered Viewer
            Thread.Sleep(100);
            BufferedViewer x = new BufferedViewer(WorkingSim);
            x.Show();
            this.Close();
        }

        private void UpdateStatus(string Message)
        {
            Status.Clear();
            Status.AppendText(Message);
        }

        private void Delay_Tick(object sender, EventArgs e)
        {
            Delay.Stop();
            if (this.IsSaving)
            {
                SaveSim();
            }
            else
            {
                LoadSim();
            }
        }
    }
}
