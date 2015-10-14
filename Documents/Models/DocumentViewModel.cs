using Foolproof;
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
        [Required(ErrorMessage = "Выбирите тип документа")]
        public int DocTypeId { get; set; }
        public string DocTypeName { get; set; }
        [Required(ErrorMessage = "Введите наименование/№ документа")]
        public string DocName { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateS { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "*")]
        [GreaterThan("DateS", ErrorMessage = "Дата 'По' должна быть больше даты 'C'")]
        public DateTime? DatePo { get; set; }
        public bool OnBoard { get; set; }
        public int CarId { get; set; }
        public SelectList DocTypesList { get; set; }
        public IEnumerable<files> FilesLink { get; set; }
        public int? Alarm1 { get; set; }
        public int? Alarm2 { get; set; }
        public string color { get; set; }
        public string period { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        [Required(ErrorMessage = "Выбирите файл")]
        public HttpPostedFileBase uploads { get; set; }
    }
}