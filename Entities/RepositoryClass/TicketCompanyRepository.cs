using Entities.Models;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RepositoryClass
{
    public class TicketCompanyRepository: GenericRepository<TicketCompany>, ITicketCompany
    {
        public TicketCompanyRepository(OrganizationContext context) : base(context)
        {

        }
    }
}
