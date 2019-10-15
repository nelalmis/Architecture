using Architecture.Base;
using Architecture.Common.Types;
using Architecture.View.Win;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.View.SystemManagement
{
    public partial class ResourceList : BrowseFormControl
    {
        #region Commands

        #region GetInfoCommand

        private DelegateCommand _GetInfoCommand;
        public DelegateCommand GetInfoCommand
        {
            get
            {
                if (_GetInfoCommand == null)
                {
                    _GetInfoCommand = new DelegateCommand(GetInfoExecute, CanGetInfoExecute);
                }
                return _GetInfoCommand;
            }
        }
        private bool CanGetInfoExecute()
        {
            return true;

        }
        private void GetInfoExecute()
        {
            ClearStatusMessage();
            ResourceRequest  request = new ResourceRequest();
            request.MethodName = "SelectByColumns";
            //request.Contract = contract;
            var response = this.Execute<ResourceRequest, GenericResponse<List<ResourceContract>>>(request);
            if (!response.Success)
            {
                ShowStatusMessage(response.Results.FirstOrDefault().Message);
                return;
            }
            this.dataGridControl1.gridControl1.DataSource  = response.Value;
            ShowStatusMessage(response.Value.Count() + " adet kayıt listelendi.");
        }

        #endregion GetInfoCommand

        #endregion Commans

        #region Properties

        #endregion Properties
        public ResourceList()
        {
            InitializeComponent();
        }
    }
}
