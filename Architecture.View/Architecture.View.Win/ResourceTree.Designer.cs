using DevExpress.XtraBars.Navigation;

namespace Architecture.View.Win
{
    partial class ResourceTree
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
            this.dynamicNavBarControl = new DevExpress.XtraNavBar.NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicNavBarControl)).BeginInit();
            this.SuspendLayout();

            //
            // dynamicNavBarControl
            //
            this.dynamicNavBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dynamicNavBarControl.Location = new System.Drawing.Point(0, 0);
            this.dynamicNavBarControl.Name = "navBarControl1";
            this.dynamicNavBarControl.OptionsNavPane.ExpandedWidth = 245;
            this.dynamicNavBarControl.Size = new System.Drawing.Size(245, 572);
            this.dynamicNavBarControl.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.SideBar;
            this.dynamicNavBarControl.TabIndex = 0;
            this.dynamicNavBarControl.Text = "navBarControl1";
            this.dynamicNavBarControl.ActiveGroupChanged += DynamicNavBarControl_ActiveGroupChanged;
            this.dynamicNavBarControl.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinNavigationPaneViewInfoRegistrator(DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName);
            
            CreateAccordions();
            
            // 
            // ResourceTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dynamicNavBarControl);      
            this.Name = "ResourceTree";
            this.Size = new System.Drawing.Size(345, 598);
            this.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dynamicNavBarControl)).EndInit();
            this.PerformLayout();
        }
        
        private void DynamicNavBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            ActiveAccordionControl = dynamicNavBarControl.ActiveGroup.CollapsedNavPaneContentControl.Controls[0] as AccordionControl;
        }
        #endregion

        public DevExpress.XtraNavBar.NavBarControl dynamicNavBarControl;
    }
}
