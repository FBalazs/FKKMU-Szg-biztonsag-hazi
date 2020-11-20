using backend.Entities;
using backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class FileService : IFileService
    {
        private readonly IDbRepository _repository;

        String caffFilesRootPath = System.IO.Directory.GetCurrentDirectory() + @"\Caffs\";

        public FileService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public List<byte[]> GetAll()
        {
            var result = _repository.GetAll<File>().ToList();
            List<byte[]> files = new List<byte[]>();

            foreach (File f in result)
            {
                //Parser-rel generáljuk
                Process process = new Process();
                process.StartInfo.FileName = "parser.exe";
                process.StartInfo.Arguments = f.FileUrl; // Note the /c command (*)
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                //* Read the output (or the error)
                string output = process.StandardOutput.ReadToEnd();
                //string err = process.StandardError.ReadToEnd();
                process.WaitForExit();

                files.Add(System.IO.File.ReadAllBytes(output));
            }


            return files;
        }

        public async Task<int> DeleteFile(int id)
        {
            var result = await _repository.FindAsync<File>(id);

            if ( result != null)
            {
                System.IO.File.Delete(result.FileUrl);
                _repository.Delete<File>(result);
            }

            return 1;
        }

        public async Task<byte[]> DownloadFile(int id)
        {
            var result = await _repository.FindAsync<File>(id);

            if (result != null)
            {
                byte[] file = System.IO.File.ReadAllBytes(result.FileUrl);
                return file;
            }

            return null;
        }

        public Task<int> UploadFile(byte[] file)
        {
            //random fájlnév generálás
            string fileName = Guid.NewGuid().ToString("n").Substring(0, 8);

            string path = caffFilesRootPath + fileName + @".caff";

            //fájl rekord létrehozás db-ben
            File file_record = new File();
            file_record.FileUrl = path;
            file_record.Name = fileName;
            file_record.Id = _repository.GetAll<File>().Count();
            file_record.Created = DateTime.Now;

            _repository.Add<File>(file_record);

            //fájl mentése
            System.IO.File.WriteAllBytes(path, file);

            return Task.FromResult(1);
        }
    }
}
