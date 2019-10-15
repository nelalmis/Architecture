using Architecture.Common.Types;
using Architecture.View.Resource;
using Architecture.View.Win;
using DevExpress.Mvvm;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Architecture.WinApplication
{
    partial class ArchitectureWinForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer dynamicComponents = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (dynamicComponents != null))
            {
                dynamicComponents.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Properties

        private RibbonControl dynamicRibbonControl;
        private RibbonPage dynamicRibbonPage;
        private RibbonPageGroup dynamicRibbonPageGroup;
        private RibbonStatusBar dynamicRibbonStatusBar;
        private SkinRibbonGalleryBarItem dynamicSkinRibbonGalleryBarItem;
        private DevExpress.XtraBars.Docking.DockManager dynamicDockManager;
        private DevExpress.XtraBars.Docking.DockPanel dynamicDockPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dynamicDockPanel_Container;

        private BarButtonItem dynamicBarButtonItem;
        private BarSubItem dynamicBarSubItem;

        public TabbedView dynamicTabbedView;
        private DevExpress.XtraBars.Docking2010.DocumentManager dynamicDocumentManager;
        private TabFormDefaultManager dynamicTabFormDefaultManager1;
        private BarDockControl dynamicBarDockControlTop;
        private BarDockControl dynamicBarDockControlBottom;
        private BarDockControl dynamicBarDockControlLeft;
        private BarDockControl dynamicBarDockControlRight;
       
        /// <summary>
        /// ResourceCode , OpenClass
        /// </summary>
        private Dictionary<string, OpenClass> leafElementDictionaryList;
        private Timer timer;

        private XtraUserControl ClosingDocument { get; set; }

        private ResourceTree tree;

        #endregion  Properties

        #region InitializeMethod
        private void InitializeComponentDynamic()
        {
            this.dynamicComponents = new System.ComponentModel.Container();
            this.dynamicRibbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.dynamicSkinRibbonGalleryBarItem = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.dynamicRibbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dynamicDockManager = new DevExpress.XtraBars.Docking.DockManager(this.dynamicComponents);
            this.dynamicDockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dynamicDockPanel_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dynamicTabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.dynamicComponents);
            this.dynamicDocumentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.dynamicComponents);
            this.dynamicTabFormDefaultManager1 = new DevExpress.XtraBars.TabFormDefaultManager();
            this.dynamicBarDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.dynamicBarDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.dynamicBarDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.dynamicBarDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDockManager)).BeginInit();
            this.dynamicDockPanel.SuspendLayout();
            this.dynamicDockPanel_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicTabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDocumentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicTabFormDefaultManager1)).BeginInit();
            this.SuspendLayout();

            leafElementDictionaryList = new Dictionary<string, OpenClass>();
            
            //
            // ResourceTRee
            //
            //TODO: Email ve Password değeri dinamikleştirilecek.
            tree = new ResourceTree("nevzat@firm.com", "123456");
            if (tree == null)
            {
                MessageBox.Show("Bir hata oluştu.");
                return;
            }
            tree.Dock = DockStyle.Fill;
            
            foreach (var item in tree.DictionaryAccordionControlElementList.Values.Select(u => u.AccordionControl).GroupBy(a => a).Select(a=>a).ToList())
            {
                item.Key.SelectedElementChanged += accordionControl_SelectedElementChanged;
                
            }

            CreateMenuNew();

            this.dynamicDockPanel_Container.Controls.Add(tree);

            //
            // Timer
            //
            timer = new Timer();
            timer.Tick += Timer_Tick;
            
            // 
            // ribbonControl
            //
            this.dynamicRibbonControl.ExpandCollapseItem.Id = 0;
            this.dynamicRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.dynamicRibbonControl.ExpandCollapseItem
            //,this.dynamicSkinRibbonGalleryBarItem
            });
            this.dynamicRibbonControl.Location = new System.Drawing.Point(0, 0);
            this.dynamicRibbonControl.MaxItemId = 48;
            this.dynamicRibbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.dynamicRibbonControl.Name = "dynamicRibbonControl";
            this.dynamicRibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.dynamicRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.dynamicRibbonControl.Size = new System.Drawing.Size(1124, 143);
            this.dynamicRibbonControl.StatusBar = this.dynamicRibbonStatusBar;
            this.dynamicRibbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // skinRibbonGalleryBarItem
            // 
            this.dynamicSkinRibbonGalleryBarItem.Id = 14;
            this.dynamicSkinRibbonGalleryBarItem.Name = "dynamicSkinRibbonGalleryBarItem";

            //TODO: template eklentisi eklenebilir.
            dynamicRibbonPageGroup.ItemLinks.Add(this.dynamicSkinRibbonGalleryBarItem);

            // 
            // ribbonStatusBar
            // 
            this.dynamicRibbonStatusBar.Location = new System.Drawing.Point(0, 568);
            this.dynamicRibbonStatusBar.Name = "ribbonStatusBar";
            this.dynamicRibbonStatusBar.Ribbon = this.dynamicRibbonControl;
            this.dynamicRibbonStatusBar.Size = new System.Drawing.Size(1124, 31);
            // 
            // dockManager
            // 
            this.dynamicDockManager.Form = this;
            this.dynamicDockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dynamicDockPanel});
            this.dynamicDockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // dockPanel
            // 
            this.dynamicDockPanel.Controls.Add(this.dynamicDockPanel_Container);
            this.dynamicDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dynamicDockPanel.ID = new System.Guid("a045df26-1503-4d9a-99c1-a531310af22b");
            this.dynamicDockPanel.Location = new System.Drawing.Point(0, 143);
            this.dynamicDockPanel.Name = "dockPanel";
            this.dynamicDockPanel.OriginalSize = new System.Drawing.Size(300, 200);
            this.dynamicDockPanel.Size = new System.Drawing.Size(300, 425);
            this.dynamicDockPanel.Text = "Modüller";
            
            // 
            // dockPanel_Container
            // 
            // this.dynamicDockPanel_Container.Controls.Add(this.dynamicAccordionControl);
            this.dynamicDockPanel_Container.Location = new System.Drawing.Point(4, 23);
            this.dynamicDockPanel_Container.Name = "dockPanel_Container";
            this.dynamicDockPanel_Container.Size = new System.Drawing.Size(191, 398);
            this.dynamicDockPanel_Container.TabIndex = 0;
           
            // 
            // tabbedView
            // 
            this.dynamicTabbedView.RootContainer.Element = null;
            
            this.dynamicTabbedView.DocumentActivated += DynamicTabbedView_DocumentActivated;
            this.dynamicTabbedView.DocumentDeactivated += DynamicTabbedView_DocumentDeactivated;
            this.dynamicTabbedView.DocumentClosing += DynamicTabbedView_DocumentClosing;
            this.dynamicTabbedView.DocumentClosed += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(this.tabbedView_DocumentClosed);
            this.dynamicTabbedView.DocumentGroupProperties.ClosePageButtonShowMode=DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
            // 
            // documentManager
            // 
            this.dynamicDocumentManager.ContainerControl = this;
            this.dynamicDocumentManager.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
            this.dynamicDocumentManager.View = this.dynamicTabbedView;
            this.dynamicDocumentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.dynamicTabbedView});
            // 
            // tabFormDefaultManager1
            // 
            this.dynamicTabFormDefaultManager1.DockControls.Add(this.dynamicBarDockControlTop);
            this.dynamicTabFormDefaultManager1.DockControls.Add(this.dynamicBarDockControlBottom);
            this.dynamicTabFormDefaultManager1.DockControls.Add(this.dynamicBarDockControlLeft);
            this.dynamicTabFormDefaultManager1.DockControls.Add(this.dynamicBarDockControlRight);
            this.dynamicTabFormDefaultManager1.Form = this;
            this.dynamicTabFormDefaultManager1.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.dynamicBarDockControlTop.CausesValidation = false;
            this.dynamicBarDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.dynamicBarDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.dynamicBarDockControlTop.Size = new System.Drawing.Size(1124, 0);
            // 
            // barDockControlBottom
            // 
            this.dynamicBarDockControlBottom.CausesValidation = false;
            this.dynamicBarDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dynamicBarDockControlBottom.Location = new System.Drawing.Point(0, 599);
            this.dynamicBarDockControlBottom.Size = new System.Drawing.Size(1124, 0);
            // 
            // barDockControlLeft
            // 
            this.dynamicBarDockControlLeft.CausesValidation = false;
            this.dynamicBarDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.dynamicBarDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.dynamicBarDockControlLeft.Size = new System.Drawing.Size(0, 599);
            // 
            // barDockControlRight
            // 
            this.dynamicBarDockControlRight.CausesValidation = false;
            this.dynamicBarDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.dynamicBarDockControlRight.Location = new System.Drawing.Point(1124, 0);
            this.dynamicBarDockControlRight.Size = new System.Drawing.Size(0, 599);

            // 
            // frmAna
            // 
            this.WindowState = FormWindowState.Maximized;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 599);
            this.Controls.Add(this.dynamicDockPanel);
            this.Controls.Add(this.dynamicRibbonStatusBar);
            this.Controls.Add(this.dynamicRibbonControl);
            this.Controls.Add(this.dynamicBarDockControlLeft);
            this.Controls.Add(this.dynamicBarDockControlRight);
            this.Controls.Add(this.dynamicBarDockControlBottom);
            this.Controls.Add(this.dynamicBarDockControlTop);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.Name = "frmAna";
            this.Ribbon = this.dynamicRibbonControl;
            this.StatusBar = this.dynamicRibbonStatusBar;
            ((System.ComponentModel.ISupportInitialize)(this.dynamicRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDockManager)).EndInit();
            this.dynamicDockPanel.ResumeLayout(false);
            this.dynamicDockPanel_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dynamicTabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDocumentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicTabFormDefaultManager1)).EndInit();
           
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        #endregion InitializeMethod

        #region  Events
        private void DynamicTabbedView_DocumentClosing(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventArgs e)
        {
            ClosingDocument = (XtraUserControl)e.Document.Control;
        }
        private void DynamicTabbedView_DocumentDeactivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            timer.Stop();
        }

        private void DynamicTabbedView_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var open = leafElementDictionaryList.Where(u => u.Key == this.ActiveControl.Name).FirstOrDefault().Value;
            if (open != null)
            {
                foreach (var item in open.DelegateCommandList)
                {
                    var isEnabled = item.Value.CanExecute(null);
                    ((BarButtonItem)item.Key).Enabled = isEnabled;
                }
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var doc = this.dynamicTabbedView.ActiveDocument;
            this.dynamicTabbedView.Controller.Close(doc);
        }

        #endregion Events

        #region Methods
        //TODO:Kullanılmıyor
        private XtraUserControl CreateUserControl(string text)
        {
            XtraUserControl result = new XtraUserControl();
            result.Name = text.ToLower() + "UserControl";
            result.Text = text;

            #region UIButtons

            DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel ActionPanel = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            // 
            // windowsUIButtonPanel
            // 
            
            ActionPanel.AppearanceButton.Hovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            ActionPanel.AppearanceButton.Hovered.FontSizeDelta = -1;
            ActionPanel.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            ActionPanel.AppearanceButton.Hovered.Options.UseBackColor = true;
            ActionPanel.AppearanceButton.Hovered.Options.UseFont = true;
            ActionPanel.AppearanceButton.Hovered.Options.UseForeColor = true;
            ActionPanel.AppearanceButton.Normal.FontSizeDelta = -1;
            ActionPanel.AppearanceButton.Normal.Options.UseFont = true;
            ActionPanel.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(159)))));
            ActionPanel.AppearanceButton.Pressed.FontSizeDelta = -1;
            ActionPanel.AppearanceButton.Pressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(159)))));
            ActionPanel.AppearanceButton.Pressed.Options.UseBackColor = true;
            ActionPanel.AppearanceButton.Pressed.Options.UseFont = true;
            ActionPanel.AppearanceButton.Pressed.Options.UseForeColor = true;
            ActionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            ActionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            ActionPanel.EnableImageTransparency = true;
            ActionPanel.ForeColor = System.Drawing.Color.White;
            ActionPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ActionPanel.Name = "windowsUIButtonPanel";
            ActionPanel.Text = "windowsUIButtonPanel";
            ActionPanel.UseButtonBackgroundImages = false;
            ActionPanel.MinimumSize = new System.Drawing.Size(60, 60);
            ActionPanel.MaximumSize = new System.Drawing.Size(0, 60);
            ActionPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
                new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
                        new DevExpress.XtraBars.Docking2010.WindowsUIButton("New", null, "New;Size32x32;GrayScaled"),
                        new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
                        new DevExpress.XtraBars.Docking2010.WindowsUIButton("Edit", null, "Edit;Size32x32;GrayScaled"),
                        new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
                        new DevExpress.XtraBars.Docking2010.WindowsUIButton("Delete", null, "Edit/Delete;Size32x32;GrayScaled"),
                        new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
                        new DevExpress.XtraBars.Docking2010.WindowsUIButton("Refresh", null, "Refresh;Size32x32;GrayScaled"),
                new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Print", null, "Preview;Size32x32;GrayScaled")
            });
            ActionPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ActionPanel.Parent = result;
            ActionPanel.Dock = DockStyle.Top;
            ActionPanel.ButtonInterval = 40;

            #endregion UIButtons

            LabelControl label = new LabelControl();
            label.Parent = result;
            label.Appearance.Font = new Font("Tahoma", 25.25F);
            label.Appearance.ForeColor = Color.Gray;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.AutoSizeMode = LabelAutoSizeMode.None;
            label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label.Text = text;

            return result;
        }

        private void AddDictionary(int resourceId, string assemblyName,string className,AccordionControlElement accordionControlElement)
        {
            OpenClass openClass = new OpenClass();
            openClass.ResourceId = resourceId;
            openClass.AssemblyName = assemblyName;
            openClass.ClassName = className;
            openClass.controlElement = accordionControlElement;
            
            leafElementDictionaryList.Add(accordionControlElement.Name, openClass);
        }

        private XtraUserControl GetUserControl(string resourceCode)
        {
            var openClass = leafElementDictionaryList.Where(u => u.Key == resourceCode).FirstOrDefault().Value;
            if (openClass.userControl == null || (openClass.userControl != null && openClass.userControl.IsDisposed))
            {
                openClass.userControl = CreateInstance(openClass, tree.ResourceList.Where(u => u.ResourceId == openClass.ResourceId).FirstOrDefault().ResourceActionList.ToList(), openClass.controlElement.Text, openClass.AssemblyName, openClass.ClassName);
            }
            return openClass.userControl;
        }

        private XtraUserControl CreateInstance(OpenClass open, List<ResourceActionContract> actionList, string text, string assemblyName, string className)
        {
            try
            {
                string fileFullFath = Constants.ClientDllPath + @"\" + assemblyName + ".dll";
                if (!File.Exists(fileFullFath))
                {
                    MessageBox.Show(fileFullFath+ " dosyası bulunamadı.");
                    return null;
                }
                var assembly = Assembly.LoadFrom(fileFullFath);
                Type type = assembly.GetType(assemblyName + "." + className);

                if (type == null)
                    return null;
                XtraUserControl userControl = (XtraUserControl)Activator.CreateInstance(type);
                userControl.Text = text;
                userControl.Name = open.controlElement.Name;

                #region Command için Hazırlandı.
                var propertiesList = ((System.Reflection.PropertyInfo[])((System.Reflection.TypeInfo)type).DeclaredProperties).Where(u => u.Name.Contains("Command")).ToList();

                var formType = typeof(BrowseFormControl);
                CommandControl buttonControl = null;
                if (type.BaseType == typeof(BrowseFormControl))
                    buttonControl = ((BrowseFormControl)userControl).commandBar;

                if (type.BaseType == typeof(TransactionFormControl))
                    buttonControl = ((TransactionFormControl)userControl).commandBar;

                string commandName;
                DelegateCommand commandValue;
                foreach (var item in propertiesList)
                {
                    commandValue = (DelegateCommand)item.GetValue(userControl);
                    commandName = item.Name.Replace("Command", "");
                    var record = actionList.Where(u => u.CommandName == commandName).FirstOrDefault();
                    if (record != null)
                    {
                        var barItem = AddCommand(buttonControl, record.DisplayName, record.Icon, record.ResourceActionId, commandValue);
                        open.DelegateCommandList.Add(barItem, commandValue);
                    }

                }
                AddStaticCommand(buttonControl);
                #endregion Command için hazırlandı.

                return userControl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private BarItem AddCommand(CommandControl buttonControl, string caption, string uri,int resourceActionId, DelegateCommand command)
        {
            DevExpress.XtraBars.BarButtonItem newButton = new DevExpress.XtraBars.BarButtonItem(buttonControl.barManager1, caption);

            newButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            newButton.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //newButton.Caption = caption;
            newButton.Glyph = Images.GetImage(uri);
            newButton.Id = resourceActionId;
            newButton.ImageIndex = 0;
            newButton.Name = command.ToString();
            newButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            newButton.Size = new System.Drawing.Size(0, 28);

            buttonControl.barCommand.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(newButton, true) });
            
            newButton.BindCommand(command);
            buttonControl.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { newButton });
            return newButton;
        }
        
        private void AddStaticCommand(Architecture.View.Win.CommandControl buttonControl)
        {
            DevExpress.XtraBars.BarButtonItem btnClose = new DevExpress.XtraBars.BarButtonItem();
            btnClose.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            btnClose.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            btnClose.Caption = "Kapat";
            btnClose.Id = 100;
            btnClose.Glyph= Images.GetImage("btn_Close");
            btnClose.Name = "btnClose";
            btnClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            btnClose.Size = new System.Drawing.Size(0, 28);
            buttonControl.barCommand.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(btnClose, true) });
            buttonControl.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { btnClose });

            btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
        }

        #region MENU ÖĞELERİ
        public void CreateMenuNew()
        {
            foreach (var tab in this.tree.TreeNodeList)
            {
                AddMenuTabItem(tab.Code, tab.Text);
                foreach (var group in tab.ChildList)
                {
                    AddMenuGroupItem(group.Code, group.Text);
                    foreach (var sub in group.ChildList)
                    {
                        AddMenuSubItem(sub.Code, sub.Text,sub.Icon);
                        foreach (var leaf in sub.ChildList)
                        {
                            AddMenuLeafItem(leaf.Code, leaf.Text,leaf.Icon);
                            var recordAccordion = tree.DictionaryAccordionControlElementList.FirstOrDefault(u => u.Key == leaf.Code);
                            var recordResource = tree.ResourceList.FirstOrDefault(u => u.Code == leaf.Code);

                            if (recordResource != null)
                            {
                                if (!string.IsNullOrEmpty(recordResource.AssemblyName))
                                    AddDictionary(leaf.Id, recordResource.AssemblyName, recordResource.ViewName, recordAccordion.Value);
                            }
                        }
                    }
                }
            }
            
            
        }        
        
        public void AddMenuTabItem(string code,string text)
        {
            dynamicRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.dynamicRibbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { dynamicRibbonPage });
            dynamicRibbonPage.Name = code;
            dynamicRibbonPage.Text = text;

        }
        public void AddMenuGroupItem(string code, string text)
        {
            this.dynamicRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.dynamicRibbonPageGroup.Name = code;
            this.dynamicRibbonPageGroup.AllowTextClipping = false;
            this.dynamicRibbonPageGroup.Text = text;
            this.dynamicRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { dynamicRibbonPageGroup });

        }
        public void AddMenuSubItem(string code, string text,string icon)
        {
            this.dynamicBarSubItem = new DevExpress.XtraBars.BarSubItem();
            this.dynamicBarSubItem.Caption = text;
            //this.dynamicBarSubItem.Id = sub.ResourceId;
            this.dynamicBarSubItem.LargeGlyph = Images.GetMenuImageAlways(icon,Images.ImageSize.Large);
            this.dynamicBarSubItem.Name = code;
            this.dynamicRibbonPageGroup.ItemLinks.Add(dynamicBarSubItem);
            this.dynamicRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] { dynamicBarSubItem });

        }
        public void AddMenuLeafItem(string code, string text, string icon)
        {
            this.dynamicBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.dynamicBarButtonItem.Caption = text;
            //this.dynamicBarButtonItem.Id = leaf.ResourceId;
            this.dynamicBarButtonItem.Glyph = Images.GetMenuImageAlways(icon,Images.ImageSize.Small);
            this.dynamicBarButtonItem.Name = code;
            this.dynamicBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dynamicBarButtonNavigation_ItemClick);
            dynamicBarSubItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(dynamicBarButtonItem) });
            this.dynamicRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] { dynamicBarButtonItem });
        }
        
        #endregion MENU UYELERİ

        #endregion Methods
        private class OpenClass
        {
            public XtraUserControl userControl { get; set; }
            public AccordionControlElement controlElement { get; set; }
            public int ResourceId { get; set; }
            public string AssemblyName { get; set; }
            public string ClassName { get; set; }
            public Dictionary<BarItem,DelegateCommand> DelegateCommandList { get; set; }
            public OpenClass()
            {
                DelegateCommandList = new Dictionary<BarItem, DelegateCommand>();
            }
           
        }
    }
}