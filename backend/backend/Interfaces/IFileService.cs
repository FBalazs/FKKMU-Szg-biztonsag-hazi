using backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IFileService
    {
        public List<string> GetAll();

        public Task<int> UploadFile(byte[] file);

        public Task<byte[]> DownloadFile(int id);

        public Task<int> DeleteFile(int id);
    }
}
