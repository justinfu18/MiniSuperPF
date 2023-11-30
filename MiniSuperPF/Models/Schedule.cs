using System;
using System.Collections.Generic;

namespace MiniSuperPF.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Services = new HashSet<Service>();
        }

        public int ScheduleId { get; set; }
        public DateTime ScheduleDateStart { get; set; }
        public DateTime ScheduleDateEnd { get; set; }
        public int InitialTime { get; set; }
        public int FinalTime { get; set; }
        public string? Notes { get; set; }
        public bool? PromoDay { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
