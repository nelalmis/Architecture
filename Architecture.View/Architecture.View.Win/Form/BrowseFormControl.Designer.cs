namespace Architecture.View.Win
{
    partial class BrowseFormControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {   
            this.components = new System.ComponentModel.Container();
            this.dataGridControl1 = new Architecture.View.Win.DataGridControl();
            this.dockPanelCriterions = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.dataGridControl1.SuspendLayout();
            this.dockPanelCriterions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // commandBar
            // 
            this.commandBar.Size = new System.Drawing.Size(1112, 53);
            // 
            // statusBar
            // 
            this.statusBar.Size = new System.Drawing.Size(1112, 25);
            // 
            // panelContainer
            // 
            this.panelContainer.Appearance.BackColor = System.Drawing.Color.White;
            this.panelContainer.Appearance.Options.UseBackColor = true;
            this.panelContainer.Controls.Add(this.dataGridControl1);
            this.panelContainer.Size = new System.Drawing.Size(1116, 455);
            // 
            // dataGridControl1
            // 
            this.dataGridControl1.Controls.Add(this.dockPanelCriterions);
            this.dataGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridControl1.Location = new System.Drawing.Point(2, 2);
            this.dataGridControl1.Name = "dataGridControl1";
            this.dataGridControl1.Size = new System.Drawing.Size(1112, 451);
            this.dataGridControl1.TabIndex = 0;
            // 
            // dockPanelCriterions
            // 
            this.dockPanelCriterions.Controls.Add(this.dockPanel1_Container);
            this.dockPanelCriterions.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelCriterions.ID = new System.Guid("e2d1d5dc-657c-4e93-95a6-0fc142bbda52");
            this.dockPanelCriterions.Location = new System.Drawing.Point(0, 0);
            this.dockPanelCriterions.Name = "dockPanelCriterions";
            this.dockPanelCriterions.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanelCriterions.Size = new System.Drawing.Size(200, 451);
            this.dockPanelCriterions.Text = "Kriterler";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(193, 423);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.dataGridControl1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelCriterions});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // BrowseFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BrowseFormControl";
            this.Size = new System.Drawing.Size(1116, 533);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.dataGridControl1.ResumeLayout(false);
            this.dockPanelCriterions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DataGridControl dataGridControl1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        public DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        public DevExpress.XtraBars.Docking.DockPanel dockPanelCriterions;
    }
}
