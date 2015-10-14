using Documents.Models;
using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documents.Repository.Implementations
{
    public class EFFileRepository : IFileRepository
    {
        DocumentsEntities dc = new DocumentsEntities();
        public IEnumerable<Models.files> GetFiles()
        {
            return dc.files;
        }

        public Models.files GetFileById(int id)
        {
            return dc.files.FirstOrDefault(x => x.id == id);
        }

        public int CreateFile(string link, string name)
        {
            files f = new files
            {
                link = link,
                name = name
            };
            return SaveFile(f);
        }

        public void DeleteFile(int id)
        {
            files file = dc.files.FirstOrDefault(x => x.id == id);
            if (file != null)
            {
                if (System.IO.File.Exists(file.link))
                {
                    System.IO.File.Delete(file.link);
                }
                dc.files.Remove(file);
            }
            dc.SaveChanges();
        }

        public int SaveFile(Models.files file)
        {
            if (file.id == 0)
                dc.files.Add(file);
            else
                dc.Entry(file).State = EntityState.Modified;
            dc.SaveChanges();
            return file.id;
        }

        public IEnumerable<files> GetFileLinksForDocument(int docId)
        {
            return dc.documents.FirstOrDefault(x => x.id == docId).files;
        }
        public int[] GetFilesForDocument(int id)
        {
            return dc.documents.FirstOrDefault(x => x.id == id).files.Select(x => x.id).ToArray();
        }


        public void DellFilesFromDocument(int docId, int[] filesId)
        {
            var doc = dc.documents.FirstOrDefault(x => x.id == docId);
            if (doc != null)
            {
                foreach (int fileId in filesId)
                {
                    var file = dc.files.FirstOrDefault(x => x.id == fileId);
                    if (file != null)
                        file.documents.Remove(doc);
                }
            }
            dc.SaveChanges();
        }
    }
}