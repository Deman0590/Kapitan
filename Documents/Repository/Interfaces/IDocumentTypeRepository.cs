using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Repository.Interfaces
{
    public interface IDocumentTypeRepository
    {
        IEnumerable<documentTypes> GetDocumentTypes();
        IEnumerable<documentTypes> RequiredDocumentTypes(int carId, int orgId);
        documentTypes GetDocumentTypeById(int id);
        void CreateDocumentType(string name, bool onBoard, int orgId, int vechicleTypeId, int? alarm1, int? alarm2, bool download, string createUser, DateTime? createDate, string updateUser, DateTime? updateDate);
        void DeleteDocumentType(int id);
        void SaveDocumentType(documentTypes docType);
    }
}
