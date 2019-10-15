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
using Architecture.View.Resource;

namespace Architecture.View.Win
{
    public partial class SearchControl : DevExpress.XtraEditors.XtraUserControl
    {
        public SearchControl()
        {
            InitializeComponent();
            this.btnDelete.Image = Images.GetImage("btn_Delete",Images.ImageSize.Medium);
            this.btnSearch.Image =  Images.GetImage("btn_Examine", Images.ImageSize.Medium);
        }

        public virtual void btnDelete_Click(object sender, EventArgs e)
        {
            textSearch.Text = "";
        }
        public virtual void btnSearch_Click(object sender, EventArgs e)
        {
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnSearch.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
