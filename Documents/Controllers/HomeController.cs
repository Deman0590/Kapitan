using Documents.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Globalization;

namespace Documents.Controllers
{
    public class HomeController : Controller
    {
        private DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public ActionResult Index()
        {
            ViewBag.List = dataManager.DocumentTypes.GetDocumentTypes();
            return RedirectToAction("AllDocuments", "Home");
        }

        public ActionResult DocTypes()
        {
            DocTypeListsViewModel docList = new DocTypeListsViewModel();
            docList.DocTypeList = new List<DocumentTypesViewModel>();
            IEnumerable<documentTypes> types = dataManager.DocumentTypes.GetDocumentTypes();
            foreach (documentTypes t in types)
            {
                docList.DocTypeList.Add(new DocumentTypesViewModel
                {
                    Id = t.id,
                    Name = t.name,
                    OnBoard = t.onBoard,
                    OrgName = (t.orgID != null) ? t.organizationLists.name : "Для всех организаций",
                    VehicleTypeName = t.vehicleTypeLists.name,
                    Alarm1 = t.alarm1,
                    Alarm2 = t.alarm2,
                    Download = t.download,
                    CreateUser = t.createUser,
                    CreateDate = t.createDate,
                    UpdateUser = t.updateUser,
                    UpdateDate = t.updateDate
                });
            }
            return View(docList);
        }

        public ActionResult AddDocType()
        {
            ViewBag.DropDownListOrg = new SelectList(dataManager.Organizations.GetOrgList(), "id", "Name", null);
            ViewBag.DropDownListVehicle = new SelectList(dataManager.VehicleTypes.GetVehicleTypes(), "id", "Name", null);
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult AddDocType(DocumentTypesViewModel model)
        {
            dataManager.DocumentTypes.CreateDocumentType(model.Name, model.OnBoard, model.OrgID, model.VehicleTypeID, model.Alarm1, model.Alarm2, model.Download, model.CreateUser, DateTime.Now, model.UpdateUser, model.UpdateDate);
            return RedirectToAction("DocTypes", "Home");
        }
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult DellDocType(int id)
        {
            documentTypes docType = dataManager.DocumentTypes.GetDocumentTypeById(id);
            return View(new DocumentTypesViewModel { Id = docType.id, Name = docType.name, OrgName = docType.organizationLists.name, VehicleTypeName = docType.vehicleTypeLists.name });
        }
        //[Authorize(Roles = "..., ..., ...")]
        [HttpPost]
        public ActionResult DellDocType(DocumentTypesViewModel model)
        {
            dataManager.DocumentTypes.DeleteDocumentType(model.Id);
            return RedirectToAction("DocTypes", "Home");
        }
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult EditDocType(int id)
        {
            documentTypes docType = dataManager.DocumentTypes.GetDocumentTypeById(id);
            ViewBag.DropDownListOrg = new SelectList(dataManager.Organizations.GetOrgList(), "id", "Name", docType.orgID);
            ViewBag.DropDownListVehicle = new SelectList(dataManager.VehicleTypes.GetVehicleTypes(), "id", "Name", docType.vehicleTypeID);
            return View(new DocumentTypesViewModel
            {
                Id = docType.id,
                Name = docType.name,
                OnBoard = docType.onBoard,
                OrgID = docType.orgID,
                VehicleTypeID = docType.vehicleTypeID,
                Alarm1 = docType.alarm1,
                Alarm2 = docType.alarm2,
                Download = docType.download,
                CreateUser = docType.createUser,
                CreateDate = docType.createDate,
                UpdateUser = docType.updateUser,
                UpdateDate = docType.updateDate
            });
        }
        //[Authorize(Roles = "..., ..., ...")]
        [HttpPost]
        public ActionResult EditDocType(DocumentTypesViewModel model)
        {
            documentTypes docType = new documentTypes
            {
                id = model.Id,
                name = model.Name,
                onBoard = model.OnBoard,
                orgID = model.OrgID,
                vehicleTypeID = model.VehicleTypeID,
                alarm1 = model.Alarm1,
                alarm2 = model.Alarm2,
                download = model.Download,
                createUser = model.CreateUser,
                createDate = model.CreateDate,
                updateUser = model.UpdateUser,
                updateDate = DateTime.Now
            };
            dataManager.DocumentTypes.SaveDocumentType(docType);
            return RedirectToAction("DocTypes", "Home");
        }
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult AddDoc(int carId)
        {
            int orgId = Convert.ToInt32(dataManager.Cars.GetCarById(carId).BranchListId);
            ViewBag.DocTypeslist = new SelectList(dataManager.DocumentTypes.GetDocumentTypesByOrg(orgId), "id", "Name", null);
            return View(new DocumentViewModel { CarId = carId });
        }
        //[Authorize(Roles = "..., ..., ...")]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> uploads, DocumentViewModel model)
        {
            int[] fileId;
            int docId;
            if (model.Id != 0)
            {
                dataManager.Documents.SaveDocument(new documents
                {
                    id = model.Id,
                    name = model.DocName,
                    docTypeID = model.DocTypeId,
                    dateS = model.DateS,
                    datePo = model.DatePo,
                    onBoard = model.OnBoard,
                    carID = model.CarId,
                    createUser = model.CreateUser,
                    createDate = model.CreateDate,
                    //updateUser = 
                    updateDate = DateTime.Now
                });
                docId = model.Id;
            }
            else
            {
                docId = dataManager.Documents.CreateDocument(model.DocTypeId, model.DocName, model.DateS, model.DatePo, model.OnBoard, model.CarId, null, DateTime.Now, null, null);
            }
            List<int> fileIdList = new List<int>();
            if (uploads != null)
            {
                foreach (var file in uploads)
                {
                    if (file != null)
                    {
                        string rout = "~/Files/" + dataManager.DocumentTypes.GetDocumentTypeById(model.DocTypeId).name + "/" + DateTime.Today.Month + "." + DateTime.Today.Year + "/";
                        if (!Directory.Exists(Server.MapPath(rout)))
                        {
                            Directory.CreateDirectory(Server.MapPath(rout));
                        }

                        // получаем имя файла
                        string fileName = System.IO.Path.GetFileName(file.FileName);
                        //проверяем наличие файла с такимже именем
                        if (System.IO.File.Exists(Server.MapPath(rout + fileName)))
                        {
                            string ext = System.IO.Path.GetExtension(file.FileName);
                            string n = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                            fileName = n + "_" + ext;
                        }
                        // сохраняем файл в папку Files в проекте
                        file.SaveAs(Server.MapPath(rout + fileName));
                        fileIdList.Add(dataManager.Files.CreateFile(Server.MapPath(rout + fileName), fileName));
                    }
                }
                fileId = fileIdList.ToArray();
                dataManager.Documents.AddDocumentsToFile(fileId, docId);
            }
            return RedirectToAction("CarList", "Home");
        }

        public FileResult Download(string Link, string Name)
        {
            return File(Link, MimeMapping.GetMimeMapping(Link), Name);
        }
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult DellDoc(int id, int carId)
        {

            documents doc = dataManager.Documents.GetDocumentById(id);
            string period;
            if (doc != null)
            {
                if (doc.dateS != null && doc.datePo != null)
                {
                    DateTime DS = Convert.ToDateTime(doc.dateS);
                    DateTime DPo = Convert.ToDateTime(doc.datePo);
                    period = DS.ToShortDateString() + " - " + DPo.ToShortDateString();
                }
                else
                {
                    period = "Бессрочный";
                }
            }
            else
            {
                return RedirectToAction("CarActiveDocumment", "Home", new { carId = carId });
            }

            return View(new DocumentViewModel
            {
                Id = doc.id,
                DocTypeName = doc.documentTypes.name,
                DocName = doc.name,
                DateS = doc.dateS,
                DatePo = doc.datePo,
                OnBoard = doc.onBoard,
                FilesLink = dataManager.Files.GetFileLinksForDocument(doc.id),
                Alarm1 = doc.documentTypes.alarm1,
                Alarm2 = doc.documentTypes.alarm2,
                period = period,
                CarId = carId
            });
        }
        //[Authorize(Roles = "..., ..., ...")]
        [HttpPost]
        public ActionResult DellDoc(DocumentViewModel model)
        {
            int[] filesId = dataManager.Files.GetFilesForDocument(model.Id);
            if (filesId.Count() != 0)
            {
                dataManager.Files.DellFilesFromDocument(model.Id, filesId);
                foreach (int fileId in filesId)
                {
                    dataManager.Files.DeleteFile(fileId);
                }
            }
            dataManager.Documents.DeleteDocument(model.Id);
            return RedirectToAction("CarActiveDocumment", "Home", new { carId = model.CarId });
        }
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult DellFile(int fileId, int docId)
        {
            List<int> filesId = new List<int>();
            filesId.Add(fileId);
            dataManager.Files.DellFilesFromDocument(docId, filesId.ToArray());
            dataManager.Files.DeleteFile(fileId);
            return View(new DocumentViewModel { FilesLink = dataManager.Files.GetFileLinksForDocument(docId) });
        }
        //[Authorize(Roles = "..., ..., ...")]
        public ActionResult EditDoc(int docId, int carId)
        {
            documents doc = dataManager.Documents.GetDocumentById(docId);
            ViewBag.DocTypeslist = new SelectList(dataManager.DocumentTypes.GetDocumentTypes(), "id", "Name", doc.docTypeID);
            return View(new DocumentViewModel
            {
                Id = doc.id,
                DocName = doc.name,
                DocTypeId = doc.docTypeID,
                DateS = doc.dateS,
                DatePo = doc.datePo,
                OnBoard = doc.onBoard,
                FilesLink = dataManager.Files.GetFileLinksForDocument(docId),
                CreateDate = doc.createDate,
                CreateUser = doc.createUser,
                CarId = carId
            });
        }
        public ActionResult CarList()
        {
            CarListViewModel carList = new CarListViewModel();
            carList.CarList = new List<CarViewModel>();
            IEnumerable<cars> cars = dataManager.Cars.GetCars();
            foreach (cars c in cars)
            {
                carList.CarList.Add(new CarViewModel
                {
                    Id = c.CarId,
                    CarNumber = c.CarNumber,
                    StateNumber = c.StateNumber,
                    ChassisNumber = c.ChassisNumber,
                    DateRelease = c.DateRelease,
                    BranchListId = c.BranchListId
                });
            }
            return View(carList);
        }
        public ActionResult CarAllDocuments(int carId)
        {
            cars car = dataManager.Cars.GetCarById(carId);
            CarAllDocumentsViewModel carDocs = new CarAllDocumentsViewModel();
            if (car != null)
            {
                carDocs.CarNumber = car.CarNumber;
                carDocs.StateNumber = car.StateNumber;
                if (car.BranchListId != null)
                    carDocs.OrgName = car.organizationLists.name;
                carDocs.DocList = new List<DocumentViewModel>();
                IEnumerable<documents> CarDocs = dataManager.Documents.GetDocumentsByCar(carId);
                foreach (documents doc in CarDocs)
                {
                    DateTime DS = Convert.ToDateTime(doc.dateS);
                    DateTime DPo = Convert.ToDateTime(doc.datePo);
                    carDocs.DocList.Add(new DocumentViewModel
                    {
                        Id = doc.id,
                        DocTypeName = doc.documentTypes.name,
                        DocName = doc.name,
                        DateS = doc.dateS,
                        DatePo = doc.datePo,
                        OnBoard = doc.onBoard,
                        FilesLink = dataManager.Files.GetFileLinksForDocument(doc.id),
                        Alarm1 = doc.documentTypes.alarm1,
                        Alarm2 = doc.documentTypes.alarm2,
                        period = DS.ToShortDateString() + " - " + DPo.ToShortDateString()
                    });
                }
            }
            return View(carDocs);
        }
        public ActionResult CarActiveDocumment(int carId)
        {
            cars car = dataManager.Cars.GetCarById(carId);
            CarActiveDocumentsViewModel ActiveDoc = new CarActiveDocumentsViewModel();
            if (car != null)
            {
                ActiveDoc.carId = car.CarId;
                ActiveDoc.CarNumber = car.CarNumber;
                ActiveDoc.StateNumber = car.StateNumber;
                if (car.BranchListId != null)
                    ActiveDoc.OrgName = car.organizationLists.name;
                ActiveDoc.DocList = new List<DocumentViewModel>();
                ActiveDoc.RequiredDocTypes = dataManager.DocumentTypes.RequiredDocumentTypes(carId, (int)car.BranchListId);
                IEnumerable<documents> CarActiveDoc = dataManager.Documents.GetActiveDocumentsByCar(carId);
                foreach (documents doc in CarActiveDoc)
                {
                    DateTime DS = Convert.ToDateTime(doc.dateS);
                    DateTime DPo = Convert.ToDateTime(doc.datePo);
                    ActiveDoc.DocList.Add(new DocumentViewModel
                    {
                        Id = doc.id,
                        DocTypeName = doc.documentTypes.name,
                        DocName = doc.name,
                        DateS = doc.dateS,
                        DatePo = doc.datePo,
                        OnBoard = doc.onBoard,
                        FilesLink = dataManager.Files.GetFileLinksForDocument(doc.id),
                        Alarm1 = doc.documentTypes.alarm1,
                        Alarm2 = doc.documentTypes.alarm2,
                        period = DS.ToShortDateString() + " - " + DPo.ToShortDateString()
                    });
                }
            }
            return View(ActiveDoc);
        }
        public ActionResult AllDocuments()
        {
            int orgId = 0;
            DateTime? Ds = null;
            DateTime? Dp = null;
            if (Request.Cookies["DocCookie"] != null)
            {
                if (Request.Cookies["DocCookie"]["org"] != "")
                {
                    orgId = Convert.ToInt32(Request.Cookies["DocCookie"]["org"]);
                }
                if (Request.Cookies["DocCookie"]["dateS"] != "")
                {
                    Ds = Convert.ToDateTime(Request.Cookies["DocCookie"]["dateS"]);
                }
                if (Request.Cookies["DocCookie"]["datePo"] != "")
                {
                    Dp = Convert.ToDateTime(Request.Cookies["DocCookie"]["datePo"]);
                }
            }
            ViewBag.DropDownListOrg = (orgId != 0) ? new SelectList(dataManager.Organizations.GetOrgList(), "id", "Name", orgId) : new SelectList(dataManager.Organizations.GetOrgList(), "id", "Name", null);
            ViewBag.DocTypes = dataManager.DocumentTypes.GetDocumentTypes();
            IEnumerable<cars> cars = (orgId != 0) ? dataManager.Cars.GetCars().Where(x => x.BranchListId == orgId) : dataManager.Cars.GetCars();
            if (cars.Count() != 0)
            {
                CarListViewModel carList = new CarListViewModel();
                if (Ds != null && Dp != null)
                {
                    carList.DateS = Ds;
                    carList.DatePo = Dp;
                }
                carList.CarList = new List<CarViewModel>();
                foreach (cars c in cars)
                {
                    int colorPrior = 0;
                    int maxColorProir = 0;
                    IEnumerable<documents> docs = (Ds != null && Dp != null) ? dataManager.Documents.GetDocumentsByCar(c.CarId).Where(x => x.datePo >= Ds && x.datePo <= Dp).OrderBy(x => x.datePo) : dataManager.Documents.GetDocumentsByCar(c.CarId).OrderBy(x => x.datePo);

                    CarViewModel car = new CarViewModel();
                    car.Id = c.CarId;
                    car.CarNumber = c.CarNumber;
                    car.StateNumber = c.StateNumber;
                    if (c.BranchListId != null)
                    {
                        car.BranchName = c.organizationLists.name;
                    }
                    IEnumerable<documentTypes> docTypes = dataManager.DocumentTypes.GetDocumentTypes();
                    if (docTypes.Count() != 0)
                    {
                        car.DocList = new List<DocumentViewModel>();
                        foreach (documentTypes dt in docTypes)
                        {
                            DocumentViewModel d = new DocumentViewModel();
                            foreach (documents doc in docs)
                            {
                                if (doc.docTypeID == dt.id)
                                {
                                    if (doc.datePo != null)
                                    {
                                        DateTime shortDate = Convert.ToDateTime(doc.datePo);
                                        shortDate.ToShortDateString();
                                        d.DocName = shortDate.ToShortDateString();
                                        d.DatePo = shortDate;
                                        int days = ((TimeSpan)(doc.datePo - DateTime.Today)).Days;
                                        if (days <= 0)
                                        {
                                            d.color = "#ff0000";
                                            colorPrior = 3;
                                        }
                                        if (days > 0 && days <= dt.alarm2)
                                        {
                                            d.color = "#ffd800";
                                            colorPrior = 2;
                                        }
                                        if (days > dt.alarm2 && days <= dt.alarm1)
                                        {
                                            d.color = "#00ff21";
                                            colorPrior = 1;
                                        }
                                    }
                                    else
                                    {
                                        d.DocName = "Есть";
                                    }
                                }
                                if (dt.orgID != c.BranchListId && dt.orgID != null)
                                {
                                    d.DocName = "Не нужно";
                                }
                            }
                            car.DocList.Add(d);
                            if (maxColorProir < colorPrior)
                                maxColorProir = colorPrior;
                        }
                    }
                    if (maxColorProir == 3) car.color = "#ff0000";
                    if (maxColorProir == 2) car.color = "#ffd800";
                    if (maxColorProir == 1) car.color = "#00ff21";
                    carList.CarList.Add(car);
                }
                return View(carList);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AllDocuments(CarListViewModel model)
        {
            HttpCookie mycook = new HttpCookie("DocCookie");

            mycook["org"] = model.OrgID.ToString();
            mycook["dateS"] = model.DateS.ToString();
            mycook["datePo"] = model.DatePo.ToString();
            Response.Cookies.Add(mycook);
            Response.Cookies["DocCookie"].Expires = DateTime.Now.AddDays(10);

            return RedirectToAction("AllDocuments", "Home");
        }
        public ActionResult CreateReport(string format)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "DocumentReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            List<ReportViewModel> l = new List<ReportViewModel>();


            IEnumerable<cars> cars = dataManager.Cars.GetCars();
            IEnumerable<documentTypes> dt = dataManager.DocumentTypes.GetDocumentTypes();
            int count = 1;
            foreach (cars car in cars)
            {
                IEnumerable<documents> docs = dataManager.Documents.GetDocumentsByCar(car.CarId);
                if (docs.Count() != 0)
                {
                    foreach (documentTypes docT in dt)
                    {
                        ReportViewModel report = new ReportViewModel();
                        report.CarId = count;
                        report.CarNumber = car.CarNumber;
                        report.StateNumber = car.StateNumber;
                        if (car.BranchListId != null)
                            report.BranchListId = car.organizationLists.name;
                        report.GroupCarId = "нет таблицы";
                        report.OwnershipName = docT.name;
                        foreach (documents d in docs)
                        {
                            if (docT.id == d.docTypeID)
                            {
                                if (d.datePo != null)
                                    report.OwnershipNote = Convert.ToDateTime(d.datePo).ToShortDateString();
                                else
                                    report.OwnershipNote = "Есть";
                            }
                            if (docT.orgID != car.BranchListId && docT.orgID != null)
                                report.OwnershipNote = "Не нужно";
                        }
                        l.Add(report);
                    }
                }
                count++;
            }

            ReportDataSource rd = new ReportDataSource("DataSet1", l);
            lr.DataSources.Add(rd);
            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + format + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
        public JsonResult Validity(int id)
        {
            documentTypes types = dataManager.DocumentTypes.GetDocumentTypeById(id);
            bool p = false;
            if (types.alarm1 != null || types.alarm2 != null)
                p = true;
            return Json(p);
        }

        public JsonResult NeedScan(int id)
        {
            documentTypes t = dataManager.DocumentTypes.GetDocumentTypeById(id);
            bool p = false;
            if (t.download == true)
                p = true;
            return Json(p);
        }
    }
}