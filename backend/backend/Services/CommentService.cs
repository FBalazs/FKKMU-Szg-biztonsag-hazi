using backend.Entities;
using backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class CommentService : ICommentService
    {
        private readonly IDbRepository _repository;

        public CommentService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public Comment DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveComment(string message)
        {
            throw new NotImplementedException();
        }
    }
}
