namespace Architecture.View.SystemManagement
{
    partial class ResourceList
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
            this.dataGridControl1.SuspendLayout();
            this.dockPanelCriterions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridControl1
            // 
            this.dataGridControl1.Size = new System.Drawing.Size(1112, 469);
            this.dataGridControl1.Controls.SetChildIndex(this.dockPanelCriterions, 0);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 442);
            // 
            // dockPanelCriterions
            // 
            this.dockPanelCriterions.Size = new System.Drawing.Size(200, 469);
            // 
            // commandBar
            // 
            this.commandBar.Size = new System.Drawing.Size(1112, 35);
            // 
            // statusBar
            // 
            this.statusBar.Size = new System.Drawing.Size(1112, 25);
            // 
            // panelContainer
            // 
            this.panelContainer.Appearance.BackColor = System.Drawing.Color.White;
            this.panelContainer.Appearance.Options.UseBackColor = true;
            this.panelContainer.Size = new System.Drawing.Size(1116, 473);
            // 
            // ResourceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ResourceList";
            this.dataGridControl1.ResumeLayout(false);
            this.dockPanelCriterions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
