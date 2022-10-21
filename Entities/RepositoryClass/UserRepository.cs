using Entities.Models;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RepositoryClass
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {     
        public UserRepository(OrganizationContext context) :base(context)
        {
           
        }

    }
}
