using Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Repository.Interfaces
{
    public interface IFileRepository
    {
        IEnumerable<files> GetFiles();
        files GetFileById(int id);
        int CreateFile(string link, string name);
        void DeleteFile(int id);
        int SaveFile(files file);
        IEnumerable<files> GetFileLinksForDocument(int docId);
        int[] GetFilesForDocument(int id);
        void DellFilesFromDocument(int docId, int[] filesId);
    }
}
