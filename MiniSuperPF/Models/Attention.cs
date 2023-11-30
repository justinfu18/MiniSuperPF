using System;
using System.Collections.Generic;

namespace MiniSuperPF.Models
{
    public partial class Attention
    {
        public Attention()
        {
            Services = new HashSet<Service>();
        }

        public int AttentionId { get; set; }
        public string AttentionDescription { get; set; } = null!;
        public int AttentionTimeSpan { get; set; }
        public decimal AttentionEstimatedCost { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
