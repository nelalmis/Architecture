using System;

namespace Architecture.Common.Types
{
    [Serializable]
    public partial class MessageRequest:RequestBase
    {
        public MessageContract Contract { get; set; }
        public MessageRequest()
        {
            Contract = new MessageContract();
        }
    }
}
