using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.Collections.Generic;

namespace Architecture.Types.SystemManagement
{
    [Serializable]
    public partial class ResourceDefinitonWindowContract:ContractBase
    {
        public List<ResourceContract> ResourceList { get; set; }
        public List<ActionContract> AllActionList { get; set; }
        public List<ComboBoxItem> MenuTypeList { get; set; }
        public List<ComboBoxItem> UITypeList { get; set; }
        public List<ComboBoxItem> ModuleList { get; set; }
        public List<ComboBoxItem> ActionTypeList { get; set; }
        public List<ComboBoxItem> ModuleNameList { get; set; }

        public ResourceDefinitonWindowContract()
        {
            ResourceList = new List<ResourceContract>();
            AllActionList = new List<ActionContract>();
            MenuTypeList = new List<ComboBoxItem>();
            UITypeList = new List<ComboBoxItem>();
            ModuleList = new List<ComboBoxItem>();
            ActionTypeList = new List<ComboBoxItem>();
            ModuleNameList = new List<ComboBoxItem>();
        }
    }
}
