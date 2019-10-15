using Architecture.Common.Types;
using Architecture.View.Win;
using DevExpress.XtraBars.Navigation;

namespace Architecture.View.SystemManagement.Popup
{
    public partial class ResourceMove : DialogFormBase
    {
        DevExpress.XtraBars.BarButtonItem ControlButtonOk { get; set; }

        private ResourceTree Tree { get; set; }

        #region SelectedAccordionElement
        
        public AccordionControlElement SelectedAccordionElement
        {
            get {
                return Tree.SelectedAccordionControlElement;
            }
        }
        public ResourceContract SelectedResourceContract
        {
            get
            {
                return Tree.SelectedResourceTreeContract;
            }
        }

        #endregion SelectedAccordionElement
        public ResourceMove()
        {
            InitializeComponent();
            ControlButtonOk = AddButton("Tamam", Resource.Images.Ok, true, ControlButtonOk_ItemClick);
            Tree = new ResourceTree("nevzat@firm.com", "123456");            
            Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Controls.Add(Tree);
            

        }

        private void ControlButtonOk_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            this.Close();
        }
    }
}
