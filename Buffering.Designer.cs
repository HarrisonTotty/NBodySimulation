namespace UniverseSimulator
{
    partial class Buffering
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buffering));
            this.PBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.NumGen = new System.Windows.Forms.NumericUpDown();
            this.StartButton = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.TextBox();
            this.CarryChanges = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumGen)).BeginInit();
            this.SuspendLayout();
            // 
            // PBar
            // 
            this.PBar.Location = new System.Drawing.Point(12, 59);
            this.PBar.Name = "PBar";
            this.PBar.Size = new System.Drawing.Size(672, 23);
            this.PBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Generations:";
            // 
            // NumGen
            // 
            this.NumGen.Location = new System.Drawing.Point(137, 7);
            this.NumGen.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.NumGen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumGen.Name = "NumGen";
            this.NumGen.Size = new System.Drawing.Size(120, 20);
            this.NumGen.TabIndex = 2;
            this.NumGen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(263, 7);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 20);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Status
            // 
            this.Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Status.Location = new System.Drawing.Point(12, 40);
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Size = new System.Drawing.Size(672, 13);
            this.Status.TabIndex = 4;
            this.Status.Text = "Ready...";
            // 
            // CarryChanges
            // 
            this.CarryChanges.AutoSize = true;
            this.CarryChanges.Location = new System.Drawing.Point(506, 10);
            this.CarryChanges.Name = "CarryChanges";
            this.CarryChanges.Size = new System.Drawing.Size(178, 17);
            this.CarryChanges.TabIndex = 5;
            this.CarryChanges.Text = "Carry over changes to main form";
            this.CarryChanges.UseVisualStyleBackColor = true;
            // 
            // Buffering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 96);
            this.Controls.Add(this.CarryChanges);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.NumGen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Buffering";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buffering...";
            this.Load += new System.EventHandler(this.Buffering_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumGen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar PBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NumGen;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox Status;
        private System.Windows.Forms.CheckBox CarryChanges;
    }
}