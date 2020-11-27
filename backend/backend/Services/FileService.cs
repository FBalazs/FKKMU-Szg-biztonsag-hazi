using backend.Entities;
using backend.Interfaces;
using backend.Models;
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
        String webpFilesRootPath = System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\images\";

        public FileService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public List<animationDto> GetAll()
        {
            var result = _repository.GetAll<File>().ToList();
            List<animationDto> files = new List<animationDto>();

            foreach (File file in result)
            {
                string outputPath = webpFilesRootPath + file.Name + @".webp";

                //ha létezik a fájl, nem generáljuk újra
                if (!System.IO.File.Exists(outputPath))
                {
                    //Parser-rel generáljuk
                    Process process = new Process();
                    process.StartInfo.FileName = "parser.exe";
                    process.StartInfo.Arguments = "-i " + file.FileUrl + " -o " + outputPath; // Note the /c command (*)
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();

                    process.WaitForExit();
                }

                animationDto f = new animationDto();
                f.id = file.Id.ToString();
                f.title = file.Name;
                f.url = @"https://localhost:8080/images/" + file.Name + @".webp";

                files.Add(f);
            }

            return files;
        }

        public async Task<int> DeleteFile(int id)
        {
            var result = await _repository.FindAsync<File>(id);

            if ( result != null)
            {
                System.IO.File.Delete(result.FileUrl);
                System.IO.File.Delete(webpFilesRootPath + result.Name + @".webp");
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
            int id = _repository.GetAll<File>().Count() + 1;
            file_record.Id = id;
            file_record.Created = DateTime.Now;

            _repository.Add<File>(file_record);

            //fájl mentése
            System.IO.File.WriteAllBytes(path, file);

            return Task.FromResult(id);
        }
    }
}
