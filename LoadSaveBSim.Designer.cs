namespace UniverseSimulator
{
    partial class LoadSaveBSim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadSaveBSim));
            this.Status = new System.Windows.Forms.TextBox();
            this.PBar = new System.Windows.Forms.ProgressBar();
            this.Delay = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Status.Location = new System.Drawing.Point(12, 12);
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Size = new System.Drawing.Size(672, 13);
            this.Status.TabIndex = 6;
            this.Status.Text = "Ready...";
            // 
            // PBar
            // 
            this.PBar.Location = new System.Drawing.Point(12, 31);
            this.PBar.Name = "PBar";
            this.PBar.Size = new System.Drawing.Size(672, 23);
            this.PBar.TabIndex = 5;
            // 
            // Delay
            // 
            this.Delay.Interval = 200;
            this.Delay.Tick += new System.EventHandler(this.Delay_Tick);
            // 
            // LoadSaveBSim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 69);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.PBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadSaveBSim";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading...";
            this.Load += new System.EventHandler(this.LoadSaveBSim_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Status;
        private System.Windows.Forms.ProgressBar PBar;
        private System.Windows.Forms.Timer Delay;
    }
}