using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;

namespace Architecture.WinApplication
{
    public partial class ArchitectureWinForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ArchitectureWinForm()
        {
            InitializeComponentDynamic();
        }
        public void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null) return;
            if (leafElementDictionaryList.FirstOrDefault(u => u.Key == e.Element.Name).Value != null)
            {
                XtraUserControl userControl = GetUserControl(e.Element.Name);
                if (userControl != null)
                {
                    dynamicTabbedView.AddDocument(userControl);
                    dynamicTabbedView.ActivateDocument(userControl);
                }
            }
        }
        void dynamicBarButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            var accordionClass = tree.DictionaryAccordionControlElementList.FirstOrDefault(u => u.Key == e.Item.Name).Value;
            if (accordionClass != null)
            {
                accordionClass.AccordionControl.SelectedElement = null;
                accordionClass.AccordionControl.SelectedElement = tree.DictionaryAccordionControlElementList.FirstOrDefault(u => u.Key == e.Item.Name).Value;
            }

        }
        void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
        {
            timer.Stop();
        }
    }
    
}