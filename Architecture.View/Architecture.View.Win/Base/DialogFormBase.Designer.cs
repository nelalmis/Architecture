namespace Architecture.View.Win
{
    partial class DialogFormBase
    {
        /// <summary>
        /// ReqViewred designer variable.
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
            this.panelContainer = new DevExpress.XtraEditors.PanelControl();
            this.statusBarControl = new Architecture.View.Win.StatusBarControl();
            this.commandControl = new Architecture.View.Win.CommandControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 45);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(643, 245);
            this.panelContainer.TabIndex = 2;
            // 
            // statusBarControl
            // 
            this.statusBarControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarControl.Location = new System.Drawing.Point(0, 290);
            this.statusBarControl.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarControl.MaximumSize = new System.Drawing.Size(5000, 25);
            this.statusBarControl.MinimumSize = new System.Drawing.Size(0, 25);
            this.statusBarControl.Name = "statusBarControl";
            this.statusBarControl.Size = new System.Drawing.Size(643, 25);
            this.statusBarControl.TabIndex = 1;
            // 
            // commandControl
            // 
            this.commandControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.commandControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandControl.Location = new System.Drawing.Point(0, 0);
            this.commandControl.MaximumSize = new System.Drawing.Size(9000, 45);
            this.commandControl.MinimumSize = new System.Drawing.Size(4, 45);
            this.commandControl.Name = "commandControl";
            this.commandControl.Size = new System.Drawing.Size(643, 45);
            this.commandControl.TabIndex = 0;
            // 
            // DialogFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 315);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.statusBarControl);
            this.Controls.Add(this.commandControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogFormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogFormBase";
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.PanelControl panelContainer;
        private CommandControl commandControl;
        private StatusBarControl statusBarControl;
    }
}