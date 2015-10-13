using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
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
                    OrgName = t.organizationLists.name,
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
        public ActionResult AddDocType(DocumentTypesViewModel model)
        {
            dataManager.DocumentTypes.CreateDocumentType(model.Name, model.OnBoard, model.OrgID, model.VehicleTypeID, model.Alarm1, model.Alarm2, model.Download, model.CreateUser, DateTime.Now, model.UpdateUser, model.UpdateDate);
            return RedirectToAction("DocTypes", "Home");
        }

        public ActionResult DellDocType(int id)
        {
            documentTypes docType = dataManager.DocumentTypes.GetDocumentTypeById(id);
            return View(new DocumentTypesViewModel { Id = docType.id, Name = docType.name, OrgName = docType.organizationLists.name, VehicleTypeName = docType.vehicleTypeLists.name });
        }
        [HttpPost]
        public ActionResult DellDocType(DocumentTypesViewModel model)
        {
            dataManager.DocumentTypes.DeleteDocumentType(model.Id);
            return RedirectToAction("DocTypes", "Home");
        }
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
        public ActionResult AddDoc(int carId)
        {
            ViewBag.DocTypeslist = new SelectList(dataManager.DocumentTypes.GetDocumentTypes(), "id", "Name", null);
            return View();
        }

        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> uploads, DocumentViewModel model)
        {

            //foreach (var file in uploads)
            //{
            //    if (file != null)
            //    {
            //        // получаем имя файла
            //        string fileName = System.IO.Path.GetFileName(file.FileName);
            //        // сохраняем файл в папку Files в проекте
            //        file.SaveAs(Server.MapPath("~/Files/" + fileName));
            //    }
            //}


        //public void AddUserToRoles(string username, string[] rolenames)
        //{
        //    var user = context.Users.FirstOrDefault(x => x.Login == username);
        //    if (user != null)
        //    {
        //        foreach (string roles in rolenames)
        //        {
        //            var role = context.Role.FirstOrDefault(x => x.RoleName == roles);
        //            if (role != null)
        //                role.Users.Add(user);
        //        }
        //    }
        //    context.SaveChanges();
        //}


            return RedirectToAction("CarList","Home");
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
        public JsonResult Validity(int id)
        {
            documentTypes types = dataManager.DocumentTypes.GetDocumentTypeById(id);
            bool p = false;
            if(types.alarm1 != null || types.alarm2 != null)
                p = true;
            return Json(p);
        }
    }
}