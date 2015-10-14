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


        public IEnumerable<documentTypes> RequiredDocumentTypes(int carId, int orgId)
        {
            //return (from dt in dc.documentTypes
            //        join d in dc.documents on dt.id equals d.docTypeID into t
            //        from rt in t.DefaultIfEmpty()
            //        where dt.onBoard == true
            //        select dt);

            //return (from dt in dc.documentTypes
            //        where dt.onBoard == true
            //        select dt into docT
            //        join doc in
            //            (from d in dc.documents where d.carID == carId && (DateTime.Equals(d.datePo, null) || d.datePo >= DateTime.Now) select d)
            //        on docT.id equals doc.docTypeID into t
            //        select docT);
            return (from dt in
                        (
                            (from DocumentTypes in dc.documentTypes
                             where
                               DocumentTypes.onBoard == true &&
                               DocumentTypes.orgID == orgId
                             select new
                             {
                                 DocumentTypes
                             }))
                    join d in
                        (
                            (from Documents1 in dc.documents
                             where
                               Documents1.carID == carId &&
                               (Documents1.datePo == null ||
                               Documents1.datePo >= DateTime.Now)
                             select new
                             {
                                 Documents1
                             })) on new { Id = dt.DocumentTypes.id } equals new { Id = d.Documents1.docTypeID } into d_join
                    from d in d_join.DefaultIfEmpty()
                    where
                      d.Documents1.id == null
                    select 
                        dt.DocumentTypes
                    );
        }
    }
}