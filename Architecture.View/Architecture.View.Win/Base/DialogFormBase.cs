using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Architecture.View.Win
{
    public partial class DialogFormBase : DevExpress.XtraEditors.XtraForm,INotifyPropertyChanged
    {
        public DialogFormBase()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public DevExpress.XtraBars.BarButtonItem AddButton(string caption, Image glyph, bool isEnabled, DevExpress.XtraBars.ItemClickEventHandler clickEvent)
        {
            DevExpress.XtraBars.BarButtonItem newBtn = new DevExpress.XtraBars.BarButtonItem();
            newBtn.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            newBtn.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            newBtn.Caption = caption;
            newBtn.Id = 100;
            newBtn.Glyph = glyph;
            newBtn.Name = "btnNew";
            newBtn.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            newBtn.Size = new System.Drawing.Size(0, 28);
            this.commandControl.barCommand.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(newBtn, true) });
            this.commandControl.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { newBtn });

            newBtn.ItemClick += clickEvent;

            return newBtn;
           
        }
    }
}
