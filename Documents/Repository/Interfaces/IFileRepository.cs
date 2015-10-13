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
        void CreateFile(string link);
        void DeleteFile(int id);
        void SaveFile(files file);
    }
}
