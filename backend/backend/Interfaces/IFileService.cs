using backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IFileService
    {
        public void UploadFile(File file);

        public File DownloadFile(String fileName);

        public int DeleteFile(int it);
    }
}
