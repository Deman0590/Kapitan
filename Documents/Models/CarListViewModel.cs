using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Documents.Models
{
    public class CarListViewModel
    {
        public List<CarViewModel> CarList { get; set; }
        public int? OrgID { get; set; }
        public DateTime? DateS { get; set; }
        [GreaterThanOrEqualTo("DateS", ErrorMessage = "Дата 'По' должна быть больше даты 'C'")]
        public DateTime? DatePo { get; set; }
    }
}