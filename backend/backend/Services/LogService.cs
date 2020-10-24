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
    }
}
