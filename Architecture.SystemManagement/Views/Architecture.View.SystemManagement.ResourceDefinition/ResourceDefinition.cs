using Architecture.Common.Types;
using Architecture.Types.SystemManagement;
using Architecture.View.SystemManagement.Popup;
using Architecture.View.Win;
using DevExpress.Mvvm;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Architecture.View.SystemManagement.ResourceDefinition
{
    public partial class ResourceDefinition : TransactionFormControl
    {
        #region Commands
        #region SaveCommand

        private DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new DelegateCommand(SaveExecute, CanSaveExecute);
                }
                return _SaveCommand;
            }
        }
        private bool CanSaveExecute()
        {
            return true;

        }
        private void SaveExecute()
        {
            ClearStatusMessage();
            
            if (!ValidateControl(textEditResourceName) && !ValidateControl(textEditResourceCodeFull) && !ValidateControl(componentResourceCode3))
                return;
            if ((IsNew && WindowContract.ResourceList.Where(u => u.Code == textEditResourceCodeFull.Text.Trim()).Any())
                || (!IsNew && WindowContract.ResourceList.Where(u => u.Code == textEditResourceCodeFull.Text.Trim() && u.ResourceId != SelectedResourceContract.ResourceId).Any())
                )
            {
                ShowStatusMessage("Girilen Resource code aynı olamaz. ", Enums.DialogTypes.Warning);
                return;
            }
            if(textEditResourceCodeFull.Text.Length!=10)
            {
                ShowStatusMessage("Resource code alanı zorunludur.", Enums.DialogTypes.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Kayıt yapılacak.", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                gridView1.MoveNextPage();
                ResourceContract contract = new ResourceContract();
                contract.AssemblyName = textEditAssemblyName.Text.Trim();
                //contract.ClassName = textEditClassName.Text.Trim();
                contract.Code = textEditResourceCodeFull.Text.Trim();
                contract.Description = textEditDescription.Text.Trim();
                contract.MenuType = WindowContract.MenuTypeList.Find(u => u.Text == comboBoxMenuType.Text).Value;
                contract.ModuleId = comboBoxModule.Text == "" ? -1 : WindowContract.ModuleList.Find(u => u.Text == comboBoxModule.Text).Value;
                contract.ParentId = -1;
                if (SelectedResourceContract != null)
                {
                    contract.ParentId = SelectedResourceContract.ParentId;
                    contract.ResourceId = SelectedResourceContract.ResourceId;
                    contract.Icon = SelectedResourceContract.Icon;
                }

                if (IsNew)
                {
                    contract.ParentId = SelectedResourceContract.ResourceId;
                    contract.Icon = "mnu_Default";
                }

                contract.SortId = textEditSortId.Text.Trim() == "" ? default(byte) : Convert.ToByte(textEditSortId.Text.Trim());
                contract.Text = textEditResourceName.Text;
                contract.ViewType = WindowContract.UITypeList.Find(u => u.Text == comboBoxUIType.Text).Value;

                gridView1.RefreshData();

                var selectedActionList = ((List<ActionContract>)gridView1.DataSource).Where(u => u.IsExists == true).ToList();

                foreach (var item in selectedActionList)
                {
                    ResourceActionContract rac = new ResourceActionContract();
                    rac.ActionId = item.ActionId;
                    rac.ActionType = item.ActionType;
                    rac.CommandName = item.CommandName;
                    rac.Description = item.Description;
                    rac.DisplayName = item.DisplayName;
                    rac.Icon = item.Icon;
                    rac.ResourceId = SelectedResourceContract.ResourceId;
                    rac.SortId = item.SortId;
                    contract.ResourceActionList.Add(rac);
                }

                Insert(contract);
            }
        }

        #endregion SaveCommand

        #region NewCommand

        private DelegateCommand _NewCommand;
        public DelegateCommand NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = new DelegateCommand(NewExecute, CanNewExecute);
                }
                return _NewCommand;
            }
        }
        private bool CanNewExecute()
        {
            if (SelectedResourceContract == null)
                return true;
            if (SelectedResourceContract != null && (SelectedResourceContract.MenuType == (short)Enums.MenuType.MenuLeaf || SelectedResourceContract.MenuType == (short)Enums.MenuType.SystemLeaf))
                return false;
            return true;
        }
        private void NewExecute()
        {
            ClearStatusMessage();
            IsNew = true;
            if (SelectedAccordionControlElement != null)
            {
                var record = WindowContract.ResourceList.Where(u => u.ResourceId == SelectedResourceContract.ResourceId).FirstOrDefault();

                SelectedResourceContract = new ResourceContract();
                SelectedResourceContract.MenuTypeName = record.MenuTypeName;
                SelectedResourceContract.ParentName = record.ParentName;

                SelectedResourceContract.MenuType = record.MenuType;
                var level = GetNodeLevel(record.ParentId);
                if ( level == (short)Enums.MenuLevel.Sub)
                    SelectedResourceContract.MenuType = record.MenuType == (short)Enums.MenuType.MenuParent ? (byte)Enums.MenuType.MenuLeaf : (byte)Enums.MenuType.SystemLeaf;

                SelectedResourceContract.ParentName = record.Text;
                SelectedResourceContract.MenuTypeName = WindowContract.MenuTypeList.Find(u => u.Value == SelectedResourceContract.MenuType).Text;
                if (SelectedResourceContract.MenuType == (short)Enums.MenuType.MenuLeaf || SelectedResourceContract.MenuType == (short)Enums.MenuType.SystemLeaf)
                {
                    gridControl1.Enabled = true;
                }
                SelectedResourceContract.ModuleName = record.ModuleName;
                SelectedResourceContract.ViewTypeName = record.ViewTypeName;
                SelectedResourceContract.ResourceId = record.ResourceId;
                SelectedResourceContract.ParentId = record.ParentId;
                 

                IsExistsSetFalse();
                gridView1.RefreshData();
                BindingProperty();
            }
            else
            {
                comboBoxMenuType.Text = WindowContract.MenuTypeList.Find(u => u.Value == (short)Enums.MenuType.MenuParent).Text; comboBoxModule.Text = "";
                comboBoxUIType.Text = Enums.ViewType.WinForm.ToString();

            }

        }

        #endregion NewCommand

        #region DeleteCommand

        private DelegateCommand _DeleteCommand;
        public DelegateCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new DelegateCommand(DeleteExecute, CanDeleteExecute);
                }
                return _DeleteCommand;
            }
        }
        private bool CanDeleteExecute()
        {
            if (SelectedResourceContract != null && !IsNew)
                return true;
            return false;
        }
        private void DeleteExecute()
        {
            ClearStatusMessage();
            DialogResult result = MessageBox.Show("Kayıt silinecek.", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ResourceRequest request = new ResourceRequest();
                request.MethodName = "Delete";
                request.Contract = SelectedResourceContract;
                var response = this.Execute<ResourceRequest, GenericResponse<Int32>>(request);
                if (!response.Success)
                {
                    ShowStatusMessage(response.Results.FirstOrDefault().Message);
                    return;
                }
                ShowStatusMessage("Silme başarılı.");
            }
        }

        #endregion DeleteCommand

        #region MoveCommand

        private DelegateCommand _MoveCommand;
        public DelegateCommand MoveCommand
        {
            get
            {
                if (_MoveCommand == null)
                {
                    _MoveCommand = new DelegateCommand(MoveExecute, CanMoveExecute);
                }
                return _MoveCommand;
            }
        }
        private bool CanMoveExecute()
        {
            return true;

        }
        private void MoveExecute()
        {
            ClearStatusMessage();
            Architecture.View.SystemManagement.ResourceDefinition.ResourceList r = new SystemManagement.ResourceList();
                
            ResourceMove moveDialog = new ResourceMove();
            
            moveDialog.ShowDialog();
            var moveAccordionElement= moveDialog.SelectedAccordionElement;
            var moveResourceContract = moveDialog.SelectedResourceContract;
            return;
            ResourceRequest request = new ResourceRequest();
            request.MethodName =  "Update";
            //request.Contract = contract;
            var response = this.Execute<ResourceRequest, GenericResponse<Int32>>(request);
            if (!response.Success)
            {
                ShowStatusMessage(response.Results.FirstOrDefault().Message);
                return;
            }


        }

        #endregion MoveCommand

        #endregion Commands

        #region Properties
        
        #region WindowContract

        private ResourceDefinitonWindowContract windowContract;
        public ResourceDefinitonWindowContract WindowContract
        {
            get { return windowContract; }
            set
            {
                windowContract = value;
                if (value != windowContract)
                {
                    OnPropertyChanged("WindowContract");
                }
            }
        }

        #endregion WindowContract
        
        #region SelectedResourceContract

        private ResourceContract selectedResourceContract;
        public ResourceContract SelectedResourceContract
        {
            get { return selectedResourceContract; }
            set
            {
                selectedResourceContract = value;
                if (value != selectedResourceContract)
                {
                    OnPropertyChanged("SelectedResourceContract");
                }
            }
        }

        #endregion SelectedResourceContract

        #region SelectedAccordionControlElement

        private AccordionControlElementBase selectedAccordionControlElement;
        public AccordionControlElementBase SelectedAccordionControlElement
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
        
        public bool IsNew { get; set; }
        #endregion Properties

        public class IconClass
        {
            public string Icon { get; set; }
            public Image IconImage { get; set; }
           
        }
        public ResourceDefinition()
        {
            InitializeComponent();           
            InitializeValues();
            ResourceTree Tree = new ResourceTree("nevzat@firm.com", "123456");
            Tree.Dock = DockStyle.Fill;
            this.groupBoxResourceTree.Controls.Add(Tree);
            //CreateResourceTree();
            gridControl1.Enabled = false;
            
        }

        #region Methods
        public void GetWindowContract()
        {
            ResourceRequest request = new ResourceRequest();
            request.MethodName = "SelectWindowBindValues";
            var response = this.Execute<ResourceRequest, GenericResponse<ResourceDefinitonWindowContract>>(request);
            if (!response.Success)
            {
                ShowStatusMessage(response.Results.FirstOrDefault().Message);
                return;
            }
            WindowContract = response.Value;
            gridControl1.DataSource = WindowContract.AllActionList;
            comboBoxMenuType.Properties.Items.AddRange(WindowContract.MenuTypeList);
            comboBoxUIType.Properties.Items.AddRange(WindowContract.UITypeList);
            comboBoxModule.Properties.Items.AddRange(WindowContract.ModuleList);
            componentResourceCode1.Properties.Items.AddRange(WindowContract.ModuleNameList);
            repositoryItemLookUpEdit1.DataSource= WindowContract.ActionTypeList;
            repositoryItemLookUpEdit1.DisplayMember = "Text";
            repositoryItemLookUpEdit1.ValueMember = "Value";

        }
        public void BindingProperty()
        {
            textEditParentResourceName.Text = SelectedResourceContract.ParentName;
            textEditResourceName.Text = SelectedResourceContract.Text;
            comboBoxMenuType.Text = SelectedResourceContract.MenuTypeName;
            textEditSortId.Text = SelectedResourceContract.SortId.ToString() ;
            comboBoxModule.Text = SelectedResourceContract.ModuleName;
            textEditAssemblyName.Text = SelectedResourceContract.AssemblyName;
            //textEditClassName.Text = SelectedResourceContract.ClassName;
            textEditDescription.Text = SelectedResourceContract.Description;
            textEditResourceCodeFull.Text = SelectedResourceContract.Code;

            componentResourceCode2.Text = "";
            componentResourceCode3.Text = "";

            if (SelectedResourceContract.Code != null)
            {
                componentResourceCode1.Text = SelectedResourceContract.Code.Substring(0, 3);
                componentResourceCode2.Text = SelectedResourceContract.Code.Substring(3, 1);
                componentResourceCode3.Text = SelectedResourceContract.Code.Substring(4, 6);
            }

            comboBoxUIType.Text = selectedResourceContract.ViewTypeName;
            
        }        
        public void IsExistsSetFalse()
        {
            foreach (var item in WindowContract.AllActionList)
            {
                item.IsExists = false;
            }
        }        
        public void InitializeValues()
        {
            GetWindowContract();
            
            List<IconClass> IconList = new List<IconClass>();
            IconClass iconClass;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            foreach (var item in WindowContract.AllActionList)
            {
                iconClass = new IconClass();
                iconClass.Icon = item.Icon;
                icon.Image =  Resource.Images.GetButtonImageAlways(item.Icon);
                IconList.Add(iconClass);
                images.AddImage(icon.Image);
            }

            repItemImageComboBoxIcon.SmallImages = images;
            var a = images.Images.Count;
            for (int i = 0; i < IconList.Count(); i++)
            {
                repItemImageComboBoxIcon.Items.Add(new ImageComboBoxItem(IconList[i].Icon, i));
            }
        }

        /// <summary>
        /// TODO: KULLANILMAYACAK
        /// </summary>
        public void CreateResourceTree()
        {
          AccordionControl dynamicAccordionControl;
         AccordionControlElement dynamicAccordionControlElementApplication;
         AccordionControlElement dynamicTabAccordionControlElement;
         AccordionControlElement dynamicGroupAccordionControlElement;
         AccordionControlElement dynamicSubAccordionControlElement;
         AccordionControlElement dynamicLeafAccordionControlElement;

            dynamicAccordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            dynamicAccordionControlElementApplication = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            dynamicAccordionControlElementApplication.Text = "Uygulamalar";
            dynamicAccordionControlElementApplication.Expanded = true;

            // 
            // accordionControl            // 
            groupBoxResourceTree.Controls.Add(dynamicAccordionControl);
            dynamicAccordionControl.AllowItemSelection = true;
            dynamicAccordionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            dynamicAccordionControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            dynamicAccordionControlElementApplication});
            dynamicAccordionControl.Location = new System.Drawing.Point(0, 0);
            dynamicAccordionControl.Name = "accordionControl";
            dynamicAccordionControl.Size = new System.Drawing.Size(191, 398);
            dynamicAccordionControl.TabIndex = 0;
            dynamicAccordionControl.Text = "accordionControl";
            dynamicAccordionControl.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.accordionControl_SelectedElementChanged);

            dynamicAccordionControl.ShowFilterControl = ShowFilterControl.Always;


            List<ResourceContract> menuTabList = WindowContract.ResourceList.Where(u => u.ParentId == -1).ToList(); //resourceList.Where(u => u.ParentId==-1 && u.MenuType== (short)Enums.MenuType.MenuTab).ToList();
            List<ResourceContract> menuGroupList = new List<ResourceContract>(); //resourceList.Where(u => u.ParentId != -1 && u.MenuType == (short)Enums.MenuType.MenuGroup).ToList();
            List<ResourceContract> menuSubItemList = new List<ResourceContract>();// = resourceList.Where(u => u.ParentId!=-1 && u.MenuType == (short)Enums.MenuType.MenuSubItem).ToList();
            List<ResourceContract> menuLeafList = new List<ResourceContract>();//resourceList.Where(u => u.ParentId !=-1 && u.MenuType == (short)Enums.MenuType.MenuLeaf).ToList();

            foreach (var tab in menuTabList)
            {
                dynamicTabAccordionControlElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
                dynamicTabAccordionControlElement.Click += AccordionControlElement_Click;
                dynamicTabAccordionControlElement.Name = tab.ResourceId.ToString();
                dynamicTabAccordionControlElement.Text = tab.Text;
                dynamicTabAccordionControlElement.Image = Resource.Images.GetMenuImageAlways(tab.Icon, Resource.Images.ImageSize.Small);
                dynamicAccordionControlElementApplication.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { dynamicTabAccordionControlElement });

                menuGroupList = FindChildrens(tab.ResourceId);
                foreach (var group in menuGroupList)
                {
                    dynamicGroupAccordionControlElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
                    dynamicGroupAccordionControlElement.Click += AccordionControlElement_Click;
                    dynamicGroupAccordionControlElement.Name = group.ResourceId.ToString();
                    dynamicGroupAccordionControlElement.Text = group.Text;
                    dynamicGroupAccordionControlElement.Image = Resource.Images.GetMenuImageAlways(group.Icon, Resource.Images.ImageSize.Small);
                    dynamicTabAccordionControlElement.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { dynamicGroupAccordionControlElement });

                    menuSubItemList = FindChildrens(group.ResourceId);
                    foreach (var sub in menuSubItemList)
                    {
                        dynamicSubAccordionControlElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
                        dynamicSubAccordionControlElement.Click += AccordionControlElement_Click;
                        dynamicSubAccordionControlElement.Name = sub.ResourceId.ToString();
                        dynamicSubAccordionControlElement.Text = sub.Text;
                        dynamicSubAccordionControlElement.Image = Resource.Images.GetMenuImageAlways(sub.Icon, Resource.Images.ImageSize.Small);
                        dynamicGroupAccordionControlElement.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { dynamicSubAccordionControlElement });

                        menuLeafList = FindChildrens(sub.ResourceId);
                        foreach (var leaf in menuLeafList)
                        {
                            //
                            dynamicLeafAccordionControlElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
                            dynamicLeafAccordionControlElement.Click += AccordionControlElement_Click;
                            dynamicLeafAccordionControlElement.Name = leaf.ResourceId.ToString();
                            dynamicLeafAccordionControlElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
                            dynamicLeafAccordionControlElement.Text = leaf.Text;
                            dynamicLeafAccordionControlElement.Image = Resource.Images.GetMenuImageAlways(leaf.Icon, Resource.Images.ImageSize.Small);
                            dynamicSubAccordionControlElement.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
                            dynamicLeafAccordionControlElement});
                        }
                    }
                }
            }

        }
        private bool ValidateControl(TextEdit text)
        {
            if (text.Text == "")
            {
                text.ErrorText = "Zorunlu alan";
                return false;
            }
            return true;
        }
        private void Insert(ResourceContract contract)
        {
            ResourceRequest request = new ResourceRequest();
            request.MethodName = IsNew?"Insert":"Update";
            request.Contract = contract;
            var response = this.Execute<ResourceRequest, GenericResponse<Int32>>(request);
            if (!response.Success)
            {
                ShowStatusMessage(response.Results.FirstOrDefault().Message);
                return;
            }
            if (SelectedResourceContract == null)
                SelectedResourceContract = new ResourceContract();
            SelectedResourceContract.ResourceId = IsNew? response.Value:SelectedResourceContract.ResourceId;

            ShowStatusMessage("Kayıt başarılı. Id: " + SelectedResourceContract.ResourceId.ToString());
            IsNew = false;
            
        }
        public List<ResourceContract> FindChildrens(int resourceId)
        {
            return WindowContract.ResourceList.Where(u => u.ParentId == resourceId && (u.MenuType == (short)Enums.MenuType.MenuLeaf || u.MenuType == (short)Enums.MenuType.MenuParent)).ToList();
        }
        public Int16 GetNodeLevel(int? parentId)
        {
            if (parentId == -1)
                return 1;
            var parent = WindowContract.ResourceList.Find(u => u.ResourceId == parentId);
            Int16 i = 1;
            while (parent.ParentId != -1)
            {
                i = Convert.ToInt16(i + 1);
                parent = WindowContract.ResourceList.Find(u => u.ResourceId == parent.ParentId);
            }
            i = Convert.ToInt16(i + 1);

            return i;
        }

        #endregion Methods

        #region Events
        private void repositoryItemTextEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == " ")
                e.Handled = true;
        }
        private void componentResourceCode3_EditValueChanged(object sender, EventArgs e)
        {
            textEditResourceCodeFull.Text = componentResourceCode1.Text + componentResourceCode2.Text + componentResourceCode3.Text;
        }
        private void text_Validating(object sender, CancelEventArgs e)
            {
            if ((sender as TextEdit).Text == "")
            {
                e.Cancel = true;
                (sender as TextEdit).ErrorText = "Zorunlu alan";
            }
        }
        private void comboBox_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as ComboBoxEdit).Text == "")
            {
                e.Cancel = true;
                (sender as ComboBoxEdit).ErrorText = "Zorunlu alan";
            }
        }
        void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null) return;

            //if (leafElementDictionaryList.FirstOrDefault(u => u.Key == e.Element.Name).Value != null)
            //{
            //XtraUserControl userControl = leafElementDictionaryList.FirstOrDefault(u => u.Key == e.Element.Name).Value.userControl;
            //if (userControl != null)
            //{
            //    dynamicTabbedView.AddDocument(userControl);
            //    dynamicTabbedView.ActivateDocument(userControl);
            //}
            //}
        }
        private void AccordionControlElement_Click(object sender, EventArgs e)
        {
            SelectedAccordionControlElement = ((DevExpress.XtraBars.Navigation.AccordionControlElementBase)sender);
            IsExistsSetFalse();
            var code = Convert.ToInt32(SelectedAccordionControlElement.Name);
            SelectedResourceContract = WindowContract.ResourceList.Where(u => u.ResourceId == code).FirstOrDefault();

            if (SelectedResourceContract != null && SelectedResourceContract.ResourceActionList != null)
            {
                gridControl1.Enabled = false;
                if (SelectedResourceContract.MenuType == (short)Enums.MenuType.MenuLeaf)
                    gridControl1.Enabled = true;
                foreach (var item in SelectedResourceContract.ResourceActionList)
                {
                    var viewRecord = WindowContract.AllActionList.Where(u => u.ActionId == item.ActionId).FirstOrDefault();
                    viewRecord.ActionType = (byte)item.ActionType;
                    viewRecord.CommandName = item.CommandName;
                    viewRecord.Description = item.Description;
                    viewRecord.DisplayName = item.DisplayName;
                    viewRecord.Icon = item.Icon;
                    viewRecord.IsExists = true;
                    viewRecord.SortId = item.SortId;
                }
            }
            BindingProperty();

            IsNew = false;
            gridView1.RefreshData();
        }
        #endregion
    }
}
