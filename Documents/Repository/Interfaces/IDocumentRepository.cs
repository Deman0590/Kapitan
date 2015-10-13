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
        IEnumerable<documents> GetDocumentsByCar(int carID);
        IEnumerable<documents> GetActiveDocumentsByCar(int carID, DateTime dateS, DateTime datePo);
        documents GetDocumentById(int id);
        void CreateDocument(int docTypeID, string name, DateTime? dateS, DateTime? datePo, bool onBoard, int carId, string createUser, DateTime? createDate, string updateUser, DateTime? updateDate);
        void DeleteDocument(int id);
        void SaveDocument(documents doc);
    }
}
