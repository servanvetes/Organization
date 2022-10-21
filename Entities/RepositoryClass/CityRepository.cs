using Entities.Models;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RepositoryClass
{
    internal class CityRepository: GenericRepository<City>, ICityRepository
    {
        public CityRepository(OrganizationContext context) : base(context)
        {

        }
    }
}
