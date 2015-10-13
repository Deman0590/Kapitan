using Documents.Models;
using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents.Repository.Implementations
{
    public class EFVehicleTypeRepository : IVehicleTypeRepository
    {
        DocumentsEntities dc = new DocumentsEntities();
        public IEnumerable<Models.vehicleTypeLists> GetVehicleTypes()
        {
            return dc.vehicleTypeLists;
        }
    }
}