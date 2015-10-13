using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string StateNumber { get; set; }
        public string ChassisNumber { get; set; }
        public int DateRelease { get; set; }
        public int? BranchListId { get; set; }
    }
}