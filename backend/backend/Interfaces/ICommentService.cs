using backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface ICommentService
    {
        public void SaveComment(String message);

        public Comment DeleteComment(int id);
    }
}
