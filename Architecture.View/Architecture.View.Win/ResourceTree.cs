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
using DevExpress.XtraBars.Navigation;
using Architecture.Common.Types;
using DevExpress.XtraNavBar;
using Architecture.View.Resource;
using Architecture.View.Root.BusinessHelper;

namespace Architecture.View.Win
{
    public partial class ResourceTree : UserControlBase
    {
        public bool[] VisibleList { get; set; }

        public int NavBarGroupCount { get { return dynamicNavBarControl.Groups.Count(); } }
        private string companyEmail { get; set; }
        private string companyPassword { get; set; }

        #region ResourceList
        private List<ResourceContract> resourceList;
        public List<ResourceContract> ResourceList
        {
            get { return resourceList; }
            set
            {
                resourceList = value;
                if (value != resourceList)
                {
                    OnPropertyChanged("ResourceList");
                }
            }
        }
        #endregion ResourceList

        #region TreeNodeList
        /// <summary>
        /// MODULENAME , TreeNodeClass
        /// </summary>
        private List<ResourceTreeNode> treeNodeList;
        public List<ResourceTreeNode> TreeNodeList
        {
            get { return treeNodeList; }
            set
            {
                treeNodeList = value;
                if (value != treeNodeList)
                {
                    OnPropertyChanged("TreeNodeList");
                }
            }
        }

        #endregion TreeNodeList

        #region SelectedResourceTreeContract

        private ResourceContract selectedResourceTreeContract;
        public ResourceContract SelectedResourceTreeContract
        {
            get { return selectedResourceTreeContract; }
            set
            {
                selectedResourceTreeContract = value;
                if (value != selectedResourceTreeContract)
                {
                    OnPropertyChanged("SelectedResourceTreeContract");
                }
            }
        }

        #endregion SelectedResourceTreeContract

        #region SelectedAccordionControlElement

        private AccordionControlElement selectedAccordionControlElement;
        public AccordionControlElement SelectedAccordionControlElement
        {
            get { return selectedAccordionControlElement; }
            set
            {
                selectedAccordionControlElement = value;
                if (value != selectedAccordionControlElement)
                {
                    OnPropertyChanged("SelectedAccordionControlElement");
                }
            }
        }

        #endregion SelectedAccordionControlElement

        #region ActiveAccordionControl

        private AccordionControl activeAccordionControl;
        public AccordionControl ActiveAccordionControl
        {
            get { if (activeAccordionControl == null) { return dynamicNavBarControl.ActiveGroup.CollapsedNavPaneContentControl.Controls[0] as AccordionControl; }else return activeAccordionControl; }
            set
            {
                activeAccordionControl = value;
                if (value != activeAccordionControl)
                {
                    OnPropertyChanged("ActiveAccordionControl");
                }
            }
        }

        #endregion ActiveAccordionControl

        #region DictionaryAccordionControlElementList
        /// <summary>
        /// code , element;
        /// </summary>
        private Dictionary<string,AccordionControlElement> dictionaryAccordionControlElementList;
        public Dictionary<string, AccordionControlElement> DictionaryAccordionControlElementList
        {
            get { return dictionaryAccordionControlElementList; }
            set
            {
                dictionaryAccordionControlElementList = value;
                if (value != dictionaryAccordionControlElementList)
                {
                    OnPropertyChanged("DictionaryAccordionControlElementList");
                }
            }
        }

        #endregion DictionaryAccordionControlElementList

        /// <summary>
        /// Module ismi / ResourceList , Uygulamalar -> ... ,Raporlar ->....
        /// </summary>
        public Dictionary<string, List<ResourceTreeNode>> DictionaryNavBarMapList { get; set; } 
               
        public ResourceTree(string email,string password)
        {
            this.companyEmail = email;
            this.companyPassword = password;
            DictionaryAccordionControlElementList = new Dictionary<string, AccordionControlElement>();
            GetResource();
            InitializeComponent();
            VisibleList = new bool[NavBarGroupCount];
            
        }

        bool isFirst=true;
        private void CreateAccordions()
        {
            foreach (var module in DictionaryNavBarMapList)
            {
                var accordion= CreateAccordionControl(module.Key);
                var header = AddAccordionHeaderElement(accordion, module.Key);
                DictionaryAccordionControlElementList.Add(module.Key, header);
                foreach (var item in module.Value)
                {
                    if (!item.IsLeaf)
                    {
                        var element = AddAccordionElement(header, ElementStyle.Group, item.Code, item.Text, item.Icon);
                        DictionaryAccordionControlElementList.Add(item.Code, element);
                        CreateChildAccordionElements(element, item.Code, item.ChildList);
                    }
                    else
                    {
                        var element = AddAccordionElement(header, ElementStyle.Item, item.Code, item.Text, item.Icon);
                        DictionaryAccordionControlElementList.Add(item.Code, element);
                    }
                }
                AddNavBarGroup(isFirst,module.Key, accordion);
                ((System.ComponentModel.ISupportInitialize)(accordion)).EndInit();
            }
            this.dynamicNavBarControl.ActiveGroup = this.dynamicNavBarControl.Groups.FirstOrDefault();
            
        }
        private void CreateChildAccordionElements(AccordionControlElement parentElement, string name, List<ResourceTreeNode> childList)
        {
            foreach (var item in childList)
            {
                AccordionControlElement newAccordionControlElement;
                if (!item.IsLeaf)
                {
                    newAccordionControlElement =  AddAccordionElement(parentElement,ElementStyle.Group,item.Code,item.Text,item.Icon);
                    DictionaryAccordionControlElementList.Add(item.Code, newAccordionControlElement);

                    CreateChildAccordionElements(newAccordionControlElement, item.Code, item.ChildList);
                }
                else
                {
                    newAccordionControlElement = AddAccordionElement(parentElement,ElementStyle.Item, item.Code, item.Text, item.Icon);
                    DictionaryAccordionControlElementList.Add(item.Code, newAccordionControlElement);
                }
            }
        }
        private AccordionControlElement AddAccordionElement(AccordionControlElement parentAccordionControlElement,ElementStyle elementStyle, string code, string text, string icon)
        {
            AccordionControlElement newAccordionControlElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            newAccordionControlElement.Name = code;
            newAccordionControlElement.Style = elementStyle;
            newAccordionControlElement.Text = text;
            newAccordionControlElement.Image = Images.GetMenuImageAlways(icon, Images.ImageSize.Small);
            parentAccordionControlElement.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
                            newAccordionControlElement});

            return newAccordionControlElement;
        }
        private AccordionControlElement AddAccordionHeaderElement(AccordionControl accordionControl, string text)
        {
            AccordionControlElement newAccordionControlElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            newAccordionControlElement.Text = text;
            newAccordionControlElement.Expanded = true;
            newAccordionControlElement.HeaderVisible = false;

            accordionControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            newAccordionControlElement});
            return newAccordionControlElement;
        }
        private AccordionControl CreateAccordionControl(string name)
        {
            AccordionControl newAccordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            ((System.ComponentModel.ISupportInitialize)(newAccordionControl)).BeginInit();

            newAccordionControl.AllowItemSelection = true;
            newAccordionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            newAccordionControl.Location = new System.Drawing.Point(0, 0);
            newAccordionControl.Name = name;
            newAccordionControl.Size = new System.Drawing.Size(191, 398);
            newAccordionControl.TabIndex = 0;
            newAccordionControl.Text = "a";
            newAccordionControl.ScrollBarMode = ScrollBarMode.Hidden;
            StyleController style = new StyleController();
            style.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            newAccordionControl.Dock = DockStyle.Fill;
            newAccordionControl.StyleController = style;
            newAccordionControl.ElementClick += AccordionControl_ElementClick;

            newAccordionControl.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.AccordionControl_SelectedElementChanged);
            newAccordionControl.ShowFilterControl = ShowFilterControl.Never;
            return newAccordionControl;
        }        
        private void AddNavBarGroup(bool isExpanded, string caption, AccordionControl accordionControl)
        {
            NavBarGroup newNavBarGroup = new NavBarGroup();
            newNavBarGroup.GroupStyle = NavBarGroupStyle.ControlContainer;
            newNavBarGroup.Caption = caption;
            newNavBarGroup.Expanded = isExpanded;
            newNavBarGroup.Visible = true;
            
            isFirst = false;
            NavBarGroupControlContainer newNavBarGroupControlContainer = new NavBarGroupControlContainer();
            SearchControl newSearchControl = new SearchControl();
            newSearchControl.btnDelete.Click += BtnDelete_Click;
            newSearchControl.btnSearch.Click += BtnSearch_Click;
            newSearchControl.Dock = DockStyle.Top;
            newSearchControl.BorderStyle = BorderStyle.FixedSingle;
          
            accordionControl.Dock = DockStyle.Fill;
            newNavBarGroupControlContainer.Controls.Add(accordionControl);
            newNavBarGroupControlContainer.Controls.Add(newSearchControl);
            newNavBarGroup.ControlContainer = newNavBarGroupControlContainer;
            newNavBarGroup.CollapsedNavPaneContentControl = newNavBarGroupControlContainer;
            this.dynamicNavBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            newNavBarGroup});
            
        }        
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SetDefaultAccordionsProperty();
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string text= (((System.Windows.Forms.Control)sender).Parent.Parent as SearchControl).textSearch.Text;
            SearchElement(text);
        }
        private void AddNavBarItem(NavBarGroup group, string caption)
        {
            NavBarItem newNavBarItem = new NavBarItem();
            newNavBarItem.Caption = caption;
            newNavBarItem.Name = caption;
            group.ItemLinks.Add(newNavBarItem);
        }
        private void AccordionControl_ElementClick(object sender, ElementClickEventArgs e)
        {
            SelectedResourceTreeContract= ResourceList.Find(u => u.Code == e.Element.Name);
            SelectedAccordionControlElement = e.Element;
        }
        private void AccordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null) return;
            SelectedAccordionControlElement = e.Element;
           
        }

        /// <summary>
        /// TODO: NavBarGroup(Ör: Uygulamalar) isimleri dinamikleştirilecek.
        /// </summary>
        private void GetResource()
        {
            var response = BusinessHelper.GetResource(companyEmail,companyPassword,null, null);
            if (!response.Success)
            {
                MessageBox.Show("Menuler yüklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ResourceList = response.Value;
            Dictionary<string, List<ResourceContract>> dictionaryGroupResource = new Dictionary<string, List<ResourceContract>>();
            dictionaryGroupResource.Add("Uygulamalar", ResourceList.Where(u => u.ViewType == (short)Enums.ViewType.WinForm).ToList());
            dictionaryGroupResource.Add("Raporlar", ResourceList.Where(u => u.ViewType == (short)Enums.ViewType.Report).ToList());

            dictionaryGroupResource.Add("Favoriler", new List<ResourceContract>());


            DictionaryNavBarMapList = new Dictionary<string, List<ResourceTreeNode>>();
            DictionaryNavBarMapList = BusinessHelper.GetAllGroupTreeNodeList(dictionaryGroupResource);
            TreeNodeList = DictionaryNavBarMapList.FirstOrDefault().Value;
            CurrentNodeMatches = new List<ResourceTreeNode>();
            CurrentNodeMatches = BusinessHelper.TreeToList(TreeNodeList);

            /*
            TreeNodeList = GetTreeNodeList(ResourceList.Where(u=>u.ViewType==(short)Enums.ViewType.WinForm).ToList());
            DictionaryNavBarMapList.Add("Uygulamalar", TreeNodeList);
            var treeNodeListReport = GetTreeNodeList(ResourceList.Where(u => u.ViewType == (short)Enums.ViewType.Report).ToList());
            DictionaryNavBarMapList.Add("Raporlar", treeNodeListReport);
            DictionaryNavBarMapList.Add("Favoriler", new List<ResourceTreeNode> { });
            TreeToList();
            */
        }

        List<AccordionControlElement> expandedNodeList;
        List<AccordionControlElement> visibleNodeList;
        private List<ResourceTreeNode> CurrentNodeMatches;

        public void SearchElement(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                expandedNodeList = new List<AccordionControlElement>();
                visibleNodeList = new List<AccordionControlElement>();

                ActiveAccordionControl.BeginUpdate();
                
                var list = CurrentNodeMatches.Where(u => u.Text.ToLower().Contains(text.ToLower())).ToList();

                expandedNodeList.AddRange(DictionaryAccordionControlElementList.Where(u => list.Exists(a => u.Key == a.Code)).Select(u=>u.Value).ToList());
                
                foreach (var item in list)
                {
                    GetAllChilds(ActiveAccordionControl, item);
                    
                    var parentNode = CurrentNodeMatches.FirstOrDefault(u => u.Id == item.ParentId);
                    while (parentNode != null)
                    {
                        expandedNodeList.Add(DictionaryAccordionControlElementList.FirstOrDefault(u => u.Value.AccordionControl == ActiveAccordionControl && u.Key==parentNode.Code).Value);
                        parentNode = CurrentNodeMatches.FirstOrDefault(u => u.Id == parentNode.ParentId);
                    }
                }

                foreach (var item in DictionaryAccordionControlElementList.Values.Where(u=>u.AccordionControl==ActiveAccordionControl).ToList())
                {
                    item.Visible = false;
                    item.Expanded = false;

                    if (expandedNodeList.Exists(u => u == item))
                    {
                        item.Expanded = true;
                        item.Visible = true;
                    }
                     if (visibleNodeList.Exists(u => u == item))
                    {
                        item.Expanded = false;
                        item.Visible = true;
                    }
                }
                ActiveAccordionControl.Elements[0].Expanded = true;
                ActiveAccordionControl.Elements[0].Visible=true;

                ActiveAccordionControl.EndUpdate();

            }
            else
            {
                SetDefaultAccordionsProperty();
            }
        }
        public void GetAllChilds(AccordionControl activeAccordion, ResourceTreeNode node)
        {
            foreach (var item in node.ChildList)
            {
                visibleNodeList.Add(DictionaryAccordionControlElementList.FirstOrDefault(u => u.Value.AccordionControl == activeAccordion && u.Key == item.Code).Value);
                GetAllChilds(activeAccordion, item);
            }
        }
        public void SetDefaultAccordionsProperty()
        {
            foreach (var item in DictionaryAccordionControlElementList.Values.Where(u=>u.AccordionControl==ActiveAccordionControl).ToList())
            {
                item.Expanded = false;
                item.Visible = true;
            }

        }
        private void ExpandSearchResult(ResourceTreeNode node)
        {
            AccordionControlElement record;
            if (node !=null)
            {
                record = DictionaryAccordionControlElementList.Where(u => u.Key == node.Code).FirstOrDefault().Value;
                if (record != null)
                {
                    record.Expanded = true;
                    record.Visible = true;
                    var parent = TreeNodeList.FirstOrDefault(u => u.Id == node.ParentId);
                    ExpandSearchResult(parent);
                }
            }
        }        
   
    }
}
