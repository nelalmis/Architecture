using System;
using static Architecture.Common.Types.Enums;

namespace Architecture.Common.Types
{
    //TODO: Bu sınıf ErrorCode
    [Serializable]
    public class Result:ContractBase
    {
        public string Exception { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMessage { get; set; }
        public Severity Severity { get; set; }
    }
}
