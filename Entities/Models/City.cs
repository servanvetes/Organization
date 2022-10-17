using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class City
    {
        public City()
        {
            Activities = new HashSet<Activity>();
        }

        public int CityId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
