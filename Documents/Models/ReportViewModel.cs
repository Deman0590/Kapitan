using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents.Models
{
    public class ReportViewModel
    {
        public int CarId { get; set; }
        public string CarNumber { get; set; }
        public string StateNumber { get; set; }
        public string BranchListId { get; set; }
        public string GroupCarId { get; set; }
        public string OwnershipName { get; set; }
        public string OwnershipNote { get; set; }
    }
}
