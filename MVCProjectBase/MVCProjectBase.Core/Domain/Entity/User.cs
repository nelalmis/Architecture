﻿using System;
using System.Collections.Generic;

namespace MVCProjectBase.Core.Domain.Entity
{
    public partial class User : BaseEntity
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public Guid ConfirmationId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeletable { get; set; }
    }
}
