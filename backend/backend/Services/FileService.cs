using backend.Entities;
using backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class FileService : IFileService
    {
        private readonly IDbRepository _repository;

        public FileService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public int DeleteFile(int it)
        {
            throw new NotImplementedException();
        }

        public File DownloadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public void UploadFile(File file)
        {
            throw new NotImplementedException();
        }
    }
}
