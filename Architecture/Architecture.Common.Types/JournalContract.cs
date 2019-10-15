using System;

namespace Architecture.Common.Types
{
    [Serializable]
   public partial class JournalContract:ContractBase
    {
        public string OperationKey { get; set; }
        public DateTime? TranDate { get; set; }
        public string TransactionName { get; set; }
        public bool? HasException { get; set; }
        public byte[] RequestBinary { get; set; }
        public RequestBase Request { get; set; }
        public byte[] ResponseBinary { get; set; }
        public ResponseBase Response { get; set; }
        public byte[] ExecutionTree { get; set; }
        public byte? LanguageId { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public DateTime? SystemDate { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime? UpdateSystemDate { get; set; }
        public string HostIp { get; set; }
        
    }
}
