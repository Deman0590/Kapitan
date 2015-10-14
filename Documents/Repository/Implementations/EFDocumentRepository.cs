using Documents.Models;
using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Documents.Repository.Implementations
{
    public class EFDocumentRepository : IDocumentRepository
    {
        DocumentsEntities dc = new DocumentsEntities();
        public IEnumerable<Models.documents> GetDocuments()
        {
            return dc.documents;
        }

        public IEnumerable<Models.documents> GetDocumentsByCar(int carID)
        {
            return dc.documents.Where(x => x.carID == carID);
        }

        public IEnumerable<Models.documents> GetActiveDocumentsByCar(int carID)
        {
            return dc.documents.Where(x => x.carID == carID && (x.datePo >= DateTime.Now || x.datePo == null));
        }

        public Models.documents GetDocumentById(int id)
        {
            return dc.documents.FirstOrDefault(x => x.id == id);
        }

        public int CreateDocument(int docTypeID, string name, DateTime? dateS, DateTime? datePo, bool onBoard, int carId, string createUser, DateTime? createDate, string updateUser, DateTime? updateDate)
        {
            documents doc = new documents
            {
                docTypeID = docTypeID,
                name = name,
                dateS = dateS,
                datePo = datePo,
                onBoard = onBoard,
                carID = carId,
                createUser = createUser,
                createDate = createDate,
                updateUser = updateUser,
                updateDate = updateDate
            };
            return SaveDocument(doc);
             
        }

        public void DeleteDocument(int id)
        {
            documents doc = dc.documents.FirstOrDefault(x => x.id == id);
            if (doc != null)
                dc.documents.Remove(doc);
            dc.SaveChanges();
        }

        public int SaveDocument(Models.documents doc)
        {
            if (doc.id == 0)
                dc.documents.Add(doc);
            else
                dc.Entry(doc).State = EntityState.Modified;
            dc.SaveChanges();
            return doc.id;

        }
        public void AddDocumentsToFile(int[] filesId, int docId)
        {
            var doc = dc.documents.FirstOrDefault(x => x.id == docId);
            if (doc != null)
            {
                foreach(int fileId in filesId)
                {
                    var file = dc.files.FirstOrDefault(x => x.id == fileId);
                    if(file != null)
                    {
                        file.documents.Add(doc);
                    }
                }
            }
            dc.SaveChanges();
        }


        public IEnumerable<documents> GetActiveDocuments()
        {
            return dc.documents.Where(x => x.datePo >= DateTime.Now);
        }
    }
}