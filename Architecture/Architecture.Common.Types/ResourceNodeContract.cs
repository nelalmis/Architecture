using System;
using System.Collections.Generic;

namespace Architecture.Common.Types
{
    public partial class ResourceTreeNode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public bool IsLeaf { get; set; }
        public List<ResourceTreeNode> ChildList { get; set; }
        public ResourceTreeNode()
        {
            ChildList = new List<ResourceTreeNode>();
        }
    }
   
}
