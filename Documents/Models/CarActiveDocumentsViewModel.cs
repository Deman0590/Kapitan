using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents.Models
{
    public class CarActiveDocumentsViewModel
    {
        public int carId { get; set; }
        public string CarNumber { get; set; }
        public string StateNumber { get; set; }
        public string OrgName { get; set; }
        public List<DocumentViewModel> DocList { get; set; }
        public IEnumerable<documentTypes> RequiredDocTypes { get; set; }
    }
}