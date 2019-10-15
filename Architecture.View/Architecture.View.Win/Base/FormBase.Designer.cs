namespace Architecture.View.Win
{
    partial class FormBase
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
            this.panelCommands = new DevExpress.XtraEditors.PanelControl();
            this.commandBar = new Architecture.View.Win.CommandControl();
            this.panelStatusBar = new DevExpress.XtraEditors.PanelControl();
            this.statusBar = new Architecture.View.Win.StatusBarControl();
            this.panelContainer = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelCommands)).BeginInit();
            this.panelCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatusBar)).BeginInit();
            this.panelStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCommands
            // 
            this.panelCommands.Controls.Add(this.commandBar);
            this.panelCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCommands.Location = new System.Drawing.Point(0, 0);
            this.panelCommands.Name = "panelCommands";
            this.panelCommands.Size = new System.Drawing.Size(1058, 35);
            this.panelCommands.TabIndex = 0;
            // 
            // commandBar
            // 
            this.commandBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.commandBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandBar.Location = new System.Drawing.Point(2, 2);
            this.commandBar.MaximumSize = new System.Drawing.Size(9000, 35);
            this.commandBar.MinimumSize = new System.Drawing.Size(4, 35);
            this.commandBar.Name = "commandBar";
            this.commandBar.Size = new System.Drawing.Size(1054, 35);
            this.commandBar.TabIndex = 0;
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.Controls.Add(this.statusBar);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 576);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Size = new System.Drawing.Size(1058, 25);
            this.panelStatusBar.TabIndex = 1;
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusBar.Location = new System.Drawing.Point(2, 2);
            this.statusBar.Margin = new System.Windows.Forms.Padding(0);
            this.statusBar.MaximumSize = new System.Drawing.Size(5000, 25);
            this.statusBar.MinimumSize = new System.Drawing.Size(0, 25);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1054, 25);
            this.statusBar.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.panelContainer.Appearance.Options.UseBackColor = true;
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 35);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1058, 541);
            this.panelContainer.TabIndex = 2;
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelCommands);
            this.Name = "FormBase";
            this.Size = new System.Drawing.Size(1058, 601);
            ((System.ComponentModel.ISupportInitialize)(this.panelCommands)).EndInit();
            this.panelCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelStatusBar)).EndInit();
            this.panelStatusBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelCommands;
        private DevExpress.XtraEditors.PanelControl panelStatusBar;
        public CommandControl commandBar;
        public StatusBarControl statusBar;
        public DevExpress.XtraEditors.PanelControl panelContainer;
    }
}
