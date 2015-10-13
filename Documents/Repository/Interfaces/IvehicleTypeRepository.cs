using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Repository.Interfaces
{
    public interface IVehicleTypeRepository
    {
        IEnumerable<vehicleTypeLists> GetVehicleTypes();
    }
}
