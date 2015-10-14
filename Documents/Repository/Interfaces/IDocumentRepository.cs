using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        IEnumerable<documents> GetDocuments();
        IEnumerable<documents> GetActiveDocuments();
        IEnumerable<documents> GetDocumentsByCar(int carID);
        IEnumerable<documents> GetActiveDocumentsByCar(int carID);
        documents GetDocumentById(int id);
        int CreateDocument(int docTypeID, string name, DateTime? dateS, DateTime? datePo, bool onBoard, int carId, string createUser, DateTime? createDate, string updateUser, DateTime? updateDate);
        void DeleteDocument(int id);
        int SaveDocument(documents doc);
        void AddDocumentsToFile(int[] filesId, int docId);
    }
}
