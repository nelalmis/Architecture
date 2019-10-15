using System;

namespace Architecture.Common.Types
{
    [Serializable]
    public partial class CompanyRequest:RequestBase
    {
        public CompanyContract Contract { get; set; }
        public CompanyRequest()
        {
            Contract = new CompanyContract();
        }
    }
}
