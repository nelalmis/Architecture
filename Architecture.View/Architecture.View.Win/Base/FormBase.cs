using Architecture.Common.Types;
using Architecture.View.Resource;
using System.Collections.Generic;
using System.ComponentModel;
using static Architecture.Common.Types.Enums;

namespace Architecture.View.Win
{
    public partial class FormBase : WindowBase
    {
        public FormBase()
        {
            InitializeComponent();
        }
        
        public void ShowStatusMessage(string message)
        {
            this.statusBar.barMessageLabel.Glyph= Images.GetImage("btn_Info", Images.ImageSize.Small);
            this.statusBar.barMessageLabel.Caption = message;
            this.statusBar.barMessageLabel.ShowImageInToolbar = true;
        }
        //TODO:Kodlanacak
        public void ShowStatusMessage(string message,DialogTypes dialogType)
        {
            this.statusBar.barMessageLabel.Glyph= Images.GetImage("btn_"+dialogType.ToString(),Images.ImageSize.Small); ;
            this.statusBar.barMessageLabel.Caption = message;
            this.statusBar.barMessageLabel.ShowImageInToolbar = true;
        }
        public void ShowStatusMessage(string message, DialogTypes dialogType,string details,string blinkMessage) { }
        public void ShowStatusMessage(string message, DialogTypes dialogType,List<Result> serverSideResults) { }

        public void ClearStatusMessage()
        {
            this.statusBar.barMessageLabel.Caption = "";
            this.statusBar.barMessageLabel.Glyph = null;
        }
        
    }
}
