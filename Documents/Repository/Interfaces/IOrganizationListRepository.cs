using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Repository.Interfaces
{
    public interface IOrganizationListRepository
    {
        IEnumerable<organizationLists> GetOrgList();
    }
}
