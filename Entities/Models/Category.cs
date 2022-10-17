using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Category
    {
        public Category()
        {
            Activities = new HashSet<Activity>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
