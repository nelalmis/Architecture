using System;

namespace Architecture.Common.Types
{
    public partial class Enums
    {
        public enum MessageSeverity
        {
            Info=1,
            Error=2,
            Warning=3
        }
        public enum DialogTypes
        {
            Info = 1,
            Error = 2,
            Warning = 3,
            Question = 4
            
        }
        public enum MenuType
        {
            MenuParent = 1,
            MenuLeaf = 2,
            SystemParent = 3,
            SystemLeaf = 4
        }

        public enum MenuLevel
        {
            Tab = 1,
            Group = 2,
            Sub = 3,
            Leaf = 4
        }
        public enum ViewType
        {
            WinForm = 1,
            WPF = 2,
            Web = 3,
            Android = 4,
            IOS = 5,
            Report = 6
        }
        public enum ActionType
        {
            İşlem = 1,
            Sil = 2,
            Diğer = 3
        }
        public enum ExecuteType
        {
            Local = 1,
            Server = 2
        }
        public enum LoggerType
        {
            Database = 1,
            File = 2,
            SMS = 3,
            Email = 4
        }
        [Serializable]
        public enum Severity
        {
            Error,
            Warning,
            Information,
            BusinessError
        }
        
    }
}
