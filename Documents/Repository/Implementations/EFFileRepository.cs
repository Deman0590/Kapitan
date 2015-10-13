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

        public void CreateFile(string link)
        {
            files f = new files 
            {
                link = link
            };
            SaveFile(f);
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
        }

        public void SaveFile(Models.files file)
        {
            if (file.id == 0)
                dc.files.Add(file);
            else
                dc.Entry(file).State = EntityState.Modified;
        }
    }
}