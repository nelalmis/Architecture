using System.Collections.Generic;

namespace MVCProjectBase.Core.Domain.Entity
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
