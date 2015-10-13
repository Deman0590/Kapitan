using Documents.Models;
using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents.Repository.Implementations
{
    public class EFOrganizationListRepository : IOrganizationListRepository
    {
        DocumentsEntities dc = new DocumentsEntities();
        public IEnumerable<Models.organizationLists> GetOrgList()
        {
            return dc.organizationLists;
        }
    }
}