using System;

namespace Architecture.Common.Types
{
    [Serializable]
    public partial class ResourceRequest:RequestBase
    {
        private ResourceContract contract;

        public ResourceContract Contract
        {
            get { return contract; }
            set { contract = value; }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? ResourceId { get; set; }
        public string ResourceCode { get; set; }

        public ResourceRequest()
        {
            Contract = new ResourceContract();
        }
    }
}
