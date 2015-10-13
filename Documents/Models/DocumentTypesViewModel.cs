using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Documents.Models
{
    public class DocumentTypesViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
        public bool OnBoard { get; set; }
        [Required(ErrorMessage = "Выбирите организацию")]
        public int OrgID { get; set; }
        public string OrgName { get; set; }
        [Required(ErrorMessage = "Выбирите тип ТС")]
        public int VehicleTypeID { get; set; }
        public string VehicleTypeName { get; set; }
        [Required(ErrorMessage = "Введите период")]
        public int? Alarm1 { get; set; }
        [Required(ErrorMessage = "Введите период")]
        public int? Alarm2 { get; set; }
        public bool Download { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}