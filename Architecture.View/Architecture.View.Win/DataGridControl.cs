namespace Architecture.View.Win
{
    public partial class DataGridControl : DevExpress.XtraEditors.XtraUserControl
    {
        public DataGridControl()
        {
            InitializeComponent();
           
            // GRİD VİEW SETTİNGS
            //gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //gridView1.GridControl = resultListControl1.gridControl1;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsView.ColumnAutoWidth = false;
        }
    }
}
