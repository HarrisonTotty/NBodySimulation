namespace UniverseSimulator
{
    partial class BufferedViewer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BufferedViewer));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.PLAY = new System.Windows.Forms.ToolStripMenuItem();
            this.STOP = new System.Windows.Forms.ToolStripMenuItem();
            this.rESETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_PlaybackOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.PlaybackOptions_RenderingSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderAll = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderOnlyVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RenderHQ = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderLQ = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Scale = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeTracker = new System.Windows.Forms.TrackBar();
            this.PrimaryGrid = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Playback = new System.Windows.Forms.Timer(this.components);
            this.SFD = new System.Windows.Forms.SaveFileDialog();
            this.loopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Loop_On = new System.Windows.Forms.ToolStripMenuItem();
            this.Loop_Off = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectiveRendering = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeTracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrimaryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PLAY,
            this.STOP,
            this.rESETToolStripMenuItem,
            this.sAVEToolStripMenuItem,
            this.Menu_PlaybackOptions,
            this.Menu_Scale});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(717, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // PLAY
            // 
            this.PLAY.Name = "PLAY";
            this.PLAY.Size = new System.Drawing.Size(47, 20);
            this.PLAY.Text = "PLAY";
            this.PLAY.Click += new System.EventHandler(this.PLAY_Click);
            // 
            // STOP
            // 
            this.STOP.Name = "STOP";
            this.STOP.Size = new System.Drawing.Size(48, 20);
            this.STOP.Text = "STOP";
            this.STOP.Click += new System.EventHandler(this.STOP_Click);
            // 
            // rESETToolStripMenuItem
            // 
            this.rESETToolStripMenuItem.Name = "rESETToolStripMenuItem";
            this.rESETToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.rESETToolStripMenuItem.Text = "RESET";
            this.rESETToolStripMenuItem.Click += new System.EventHandler(this.rESETToolStripMenuItem_Click);
            // 
            // sAVEToolStripMenuItem
            // 
            this.sAVEToolStripMenuItem.Name = "sAVEToolStripMenuItem";
            this.sAVEToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.sAVEToolStripMenuItem.Text = "SAVE";
            this.sAVEToolStripMenuItem.Click += new System.EventHandler(this.sAVEToolStripMenuItem_Click);
            // 
            // Menu_PlaybackOptions
            // 
            this.Menu_PlaybackOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlaybackOptions_RenderingSettings,
            this.loopToolStripMenuItem});
            this.Menu_PlaybackOptions.Name = "Menu_PlaybackOptions";
            this.Menu_PlaybackOptions.Size = new System.Drawing.Size(111, 20);
            this.Menu_PlaybackOptions.Text = "Playback Options";
            // 
            // PlaybackOptions_RenderingSettings
            // 
            this.PlaybackOptions_RenderingSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RenderAll,
            this.SelectiveRendering,
            this.RenderOnlyVisible,
            this.toolStripSeparator2,
            this.RenderHQ,
            this.RenderLQ});
            this.PlaybackOptions_RenderingSettings.Name = "PlaybackOptions_RenderingSettings";
            this.PlaybackOptions_RenderingSettings.Size = new System.Drawing.Size(173, 22);
            this.PlaybackOptions_RenderingSettings.Text = "Rendering Settings";
            // 
            // RenderAll
            // 
            this.RenderAll.Enabled = false;
            this.RenderAll.Name = "RenderAll";
            this.RenderAll.Size = new System.Drawing.Size(177, 22);
            this.RenderAll.Text = "All";
            this.RenderAll.Click += new System.EventHandler(this.RenderAll_Click);
            // 
            // RenderOnlyVisible
            // 
            this.RenderOnlyVisible.Name = "RenderOnlyVisible";
            this.RenderOnlyVisible.Size = new System.Drawing.Size(177, 22);
            this.RenderOnlyVisible.Text = "Only Visible";
            this.RenderOnlyVisible.Click += new System.EventHandler(this.RenderOnlyVisible_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
            // 
            // RenderHQ
            // 
            this.RenderHQ.Enabled = false;
            this.RenderHQ.Name = "RenderHQ";
            this.RenderHQ.Size = new System.Drawing.Size(177, 22);
            this.RenderHQ.Text = "High Quality";
            this.RenderHQ.Click += new System.EventHandler(this.RenderHQ_Click);
            // 
            // RenderLQ
            // 
            this.RenderLQ.Name = "RenderLQ";
            this.RenderLQ.Size = new System.Drawing.Size(177, 22);
            this.RenderLQ.Text = "Low Quality";
            this.RenderLQ.Click += new System.EventHandler(this.RenderLQ_Click);
            // 
            // Menu_Scale
            // 
            this.Menu_Scale.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8});
            this.Menu_Scale.Name = "Menu_Scale";
            this.Menu_Scale.Size = new System.Drawing.Size(46, 20);
            this.Menu_Scale.Text = "Scale";
            // 
            // fitToolStripMenuItem
            // 
            this.fitToolStripMenuItem.Name = "fitToolStripMenuItem";
            this.fitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.fitToolStripMenuItem.Text = "Fit";
            this.fitToolStripMenuItem.Click += new System.EventHandler(this.fitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem2.Text = "50";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem3.Text = "100";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem4.Text = "500 (Default)";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem5.Text = "1000";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem6.Text = "5000";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem7.Text = "10000";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem8.Text = "50000";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // TimeTracker
            // 
            this.TimeTracker.BackColor = System.Drawing.Color.White;
            this.TimeTracker.Dock = System.Windows.Forms.DockStyle.Top;
            this.TimeTracker.Location = new System.Drawing.Point(0, 24);
            this.TimeTracker.Name = "TimeTracker";
            this.TimeTracker.Size = new System.Drawing.Size(717, 45);
            this.TimeTracker.TabIndex = 1;
            this.TimeTracker.Scroll += new System.EventHandler(this.TimeTracker_Scroll);
            this.TimeTracker.ValueChanged += new System.EventHandler(this.TimeTracker_ValueChanged);
            // 
            // PrimaryGrid
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Maximum = 500D;
            chartArea1.AxisX.Minimum = -500D;
            chartArea1.AxisX.Title = "X Position";
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.Maximum = 500D;
            chartArea1.AxisY.Minimum = -500D;
            chartArea1.AxisY.Title = "Y Position";
            chartArea1.Name = "PrimaryChartArea";
            this.PrimaryGrid.ChartAreas.Add(chartArea1);
            this.PrimaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrimaryGrid.Location = new System.Drawing.Point(0, 69);
            this.PrimaryGrid.Name = "PrimaryGrid";
            series1.ChartArea = "PrimaryChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series1.IsVisibleInLegend = false;
            series1.Name = "PrimarySeries";
            series1.SmartLabelStyle.Enabled = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.PrimaryGrid.Series.Add(series1);
            this.PrimaryGrid.Size = new System.Drawing.Size(717, 359);
            this.PrimaryGrid.TabIndex = 2;
            title1.Name = "GenerationCount";
            title1.Text = "Generation 0";
            this.PrimaryGrid.Titles.Add(title1);
            // 
            // Playback
            // 
            this.Playback.Interval = 1;
            this.Playback.Tick += new System.EventHandler(this.Playback_Tick);
            // 
            // SFD
            // 
            this.SFD.Filter = "Buffered Simulation Files|*.bsim|All Files|*.*";
            this.SFD.Title = "Save Buffered Simulation...";
            this.SFD.FileOk += new System.ComponentModel.CancelEventHandler(this.SFD_FileOk);
            // 
            // loopToolStripMenuItem
            // 
            this.loopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Loop_On,
            this.Loop_Off});
            this.loopToolStripMenuItem.Name = "loopToolStripMenuItem";
            this.loopToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.loopToolStripMenuItem.Text = "Loop";
            // 
            // Loop_On
            // 
            this.Loop_On.Name = "Loop_On";
            this.Loop_On.Size = new System.Drawing.Size(152, 22);
            this.Loop_On.Text = "ON";
            this.Loop_On.Click += new System.EventHandler(this.Loop_On_Click);
            // 
            // Loop_Off
            // 
            this.Loop_Off.Enabled = false;
            this.Loop_Off.Name = "Loop_Off";
            this.Loop_Off.Size = new System.Drawing.Size(152, 22);
            this.Loop_Off.Text = "OFF";
            this.Loop_Off.Click += new System.EventHandler(this.Loop_Off_Click);
            // 
            // SelectiveRendering
            // 
            this.SelectiveRendering.Name = "SelectiveRendering";
            this.SelectiveRendering.Size = new System.Drawing.Size(177, 22);
            this.SelectiveRendering.Text = "Selective Rendering";
            this.SelectiveRendering.Click += new System.EventHandler(this.SelectiveRendering_Click);
            // 
            // BufferedViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 428);
            this.Controls.Add(this.PrimaryGrid);
            this.Controls.Add(this.TimeTracker);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "BufferedViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buffered Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BufferedViewer_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeTracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrimaryGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem PLAY;
        private System.Windows.Forms.ToolStripMenuItem STOP;
        private System.Windows.Forms.DataVisualization.Charting.Chart PrimaryGrid;
        private System.Windows.Forms.Timer Playback;
        public System.Windows.Forms.TrackBar TimeTracker;
        private System.Windows.Forms.ToolStripMenuItem rESETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVEToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SFD;
        private System.Windows.Forms.ToolStripMenuItem Menu_Scale;
        private System.Windows.Forms.ToolStripMenuItem fitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem Menu_PlaybackOptions;
        private System.Windows.Forms.ToolStripMenuItem PlaybackOptions_RenderingSettings;
        private System.Windows.Forms.ToolStripMenuItem RenderAll;
        private System.Windows.Forms.ToolStripMenuItem RenderOnlyVisible;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem RenderHQ;
        private System.Windows.Forms.ToolStripMenuItem RenderLQ;
        private System.Windows.Forms.ToolStripMenuItem loopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Loop_On;
        private System.Windows.Forms.ToolStripMenuItem Loop_Off;
        private System.Windows.Forms.ToolStripMenuItem SelectiveRendering;
    }
}