namespace Architecture.View.Win
{
    public partial class BrowseFormControl : FormBase
    {   
        public BrowseFormControl()
        {
            InitializeComponent();
        }

        public void AddGridColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView, string fieldName, string columnHeader, bool isVisible, int index)
        {
            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn.FieldName = fieldName;
            ///gridColumn.Name = fieldName;
            gridColumn.Caption = columnHeader;
            gridColumn.Visible = isVisible;
            gridColumn.VisibleIndex = index;
            gridView.Columns.Add(gridColumn);
        }
       
    }
}
