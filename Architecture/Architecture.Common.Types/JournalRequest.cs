using System;

namespace Architecture.Common.Types
{
    [Serializable]
    public partial class JournalRequest:RequestBase
    {
        public JournalContract Contract { get; set; }
        public JournalRequest()
        {
            Contract = new JournalContract();
        }
    }
}
