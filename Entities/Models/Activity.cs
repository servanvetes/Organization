using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Activity
    {
        public Activity()
        {
            UserActivities = new HashSet<UserActivity>();
        }

        public int ActivityId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime HappenedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public string Description { get; set; } = null!;
        public int CityId { get; set; }
        public string Address { get; set; } = null!;
        public int Quota { get; set; }
        public bool IsTicketed { get; set; }
        public int CategoryId { get; set; }
        public decimal? TicketPrice { get; set; }
        public int CreatUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Passive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual City City { get; set; } = null!;
        public virtual ICollection<UserActivity> UserActivities { get; set; }
    }
}
