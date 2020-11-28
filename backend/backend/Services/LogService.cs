using backend.Entities;
using backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class LogService : ILogService
    {
        private readonly IDbRepository _repository;

        public LogService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public void Logger(string userId, string activity, string dbName, string id = "")
        {
            //dátum, user neve, tevékenység, melyik db-n, mit,
            string logText = userId + ", " + activity + ", " + dbName + ", " + id;
            Log log = new Log();
            log.Created = DateTime.Now;
            log.LogText = logText;
            _repository.Add<Log>(log);
            _repository.SaveAsync();
        }
    }
}
