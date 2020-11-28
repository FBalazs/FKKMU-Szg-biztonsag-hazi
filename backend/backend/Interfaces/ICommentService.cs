using backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface ICommentService
    {
        public List<Comment> GetAllComment(int animation_id);

        public Task<int> SaveComment(Comment comment);

        public Task<int> DeleteComment(int id);
    }
}
