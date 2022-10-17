using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class UserActivity
    {
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
