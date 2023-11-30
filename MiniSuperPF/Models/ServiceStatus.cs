using System;
using System.Collections.Generic;

namespace MiniSuperPF.Models
{
    public partial class ServiceStatus
    {
        public ServiceStatus()
        {
            Services = new HashSet<Service>();
        }

        public int ServiceStatusId { get; set; }
        public string ServiceStatusDescription { get; set; } = null!;

        public virtual ICollection<Service> Services { get; set; }
    }
}
