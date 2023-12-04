using System;
using System.Collections.Generic;

namespace MiniSuperPF.Models
{
    public partial class User
    {
        public User()
        {
            Services = new HashSet<Service>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? CardId { get; set; }
        public string LoginPassword { get; set; } = null!;
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int UserRoleId { get; set; }
        public int UserStatusId { get; set; }

        public virtual UserRole? UserRole { get; set; } = null!;
        public virtual UserStatus? UserStatus { get; set; } = null!;
        public virtual ICollection<Service>? Services { get; set; }
    }
}
