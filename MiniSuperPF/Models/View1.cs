using System;
using System.Collections.Generic;

namespace MiniSuperPF.Models
{
    public partial class View1
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? CardId { get; set; }
        public string LoginPassword { get; set; } = null!;
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int UserRoleId { get; set; }
        public string UserRoleDescription { get; set; } = null!;
        public int UserStatusId { get; set; }
        public string UserStatusDescription { get; set; } = null!;
    }
}
