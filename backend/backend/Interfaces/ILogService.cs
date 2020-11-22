using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface ILogService
    {
        public void Logger(string userId, string activity, string dbName, string id = "");
    }
}
