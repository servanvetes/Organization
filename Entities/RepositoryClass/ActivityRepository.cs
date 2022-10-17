using Entities.Models;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RepositoryClass
{
    public class ActivityRepository: GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(OrganizationContext context) : base(context)
        {

        }
    }

  
}
