using System;
using System.Collections.Generic;

namespace MiniSuperPF.Models
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public int AttentionId { get; set; }
        public int ScheduleId { get; set; }
        public int ServiceStatusId { get; set; }
        public DateTime ServiceDate { get; set; }
        public int? ServiceStart { get; set; }
        public int? ServiceEnd { get; set; }

        public virtual Attention Attention { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
        public virtual ServiceStatus ServiceStatus { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
