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
using Architecture.View.Win.Base;

namespace Architecture.View.Win.Components
{
    public partial class TextEditorLabeled : EditorBase
    {
        
        public TextEditorLabeled(string name,int? height)
        {
            InitializeComponent();
        }

        private void TextEditorLabeled_Load(object sender, EventArgs e)
        {

        }
    }
}
