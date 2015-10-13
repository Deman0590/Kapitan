using Documents.Models;
using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Documents.Repository.Implementations
{
    public class EFDocumentTypeRepository : IDocumentTypeRepository
    {
        DocumentsEntities dc = new DocumentsEntities();
        public IEnumerable<Models.documentTypes> GetDocumentTypes()
        {
            return dc.documentTypes;
        }

        public Models.documentTypes GetDocumentTypeById(int id)
        {
            return dc.documentTypes.FirstOrDefault(x => x.id == id);
        }

        public void CreateDocumentType(string name, bool onBoard, int orgId, int vechicleTypeId, int? alarm1, int? alarm2, bool download, string createUser, DateTime? createDate, string updateUser, DateTime? updateDate)
        {
            documentTypes docType = new documentTypes
            {
                name = name,
                onBoard = onBoard,
                orgID = orgId,
                vehicleTypeID = vechicleTypeId,
                alarm1 = alarm1,
                alarm2 = alarm2,
                download = download,
                createUser = createUser,
                createDate = createDate,
                updateUser = updateUser,
                updateDate = updateDate
            };
            SaveDocumentType(docType);
        }

        public void DeleteDocumentType(int id)
        {
            documentTypes docType = dc.documentTypes.FirstOrDefault(x => x.id == id);
            if (docType != null)
                dc.documentTypes.Remove(docType);
            dc.SaveChanges();
        }

        public void SaveDocumentType(Models.documentTypes docType)
        {
            if (docType.id == 0)
                dc.documentTypes.Add(docType);
            else
                dc.Entry(docType).State = EntityState.Modified;
            dc.SaveChanges();
        }
    }
}