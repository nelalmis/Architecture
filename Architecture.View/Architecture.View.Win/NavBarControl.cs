using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;

namespace Architecture.View.Win
{
    public partial class NavBarControl : UserControlBase
    {

        public NavBarControl()
        {
            InitializeComponent();
            NavBarGroupControlContainer newNavBarGroupControlContainer = new NavBarGroupControlContainer();
            SearchControl newSearchControl = new SearchControl();
            newSearchControl.Dock = DockStyle.Top;
            newSearchControl.BorderStyle = BorderStyle.FixedSingle;

            newNavBarGroupControlContainer.Controls.Add(newSearchControl);
            navBarGroup1.ControlContainer = newNavBarGroupControlContainer;
        }
    }
}
