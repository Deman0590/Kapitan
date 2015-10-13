using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documents.Models
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Выбирите тип документа")]
        public int DocTypeId { get; set; }
        public string DocName { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? DateS { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? DatePo { get; set; }
        public bool OnBoard { get; set; }
        public SelectList DocTypesList { get; set; }
    }
}