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

        public List<Comment> GetAllComment(int animation_id)
        {
            List<Comment> comments = _repository.GetAll<Comment>().ToList();

            List<Comment> commentsByAnimationId = comments.Where(x => x.FileId == animation_id).ToList();

            return commentsByAnimationId;
        }

        public async Task<int> DeleteComment(int id)
        {
            var comment = await _repository.FindAsync<Comment>(id);

            _repository.Delete(comment);

            return 1;
        }

        public Task<int> SaveComment(Comment comment)
        {
            _repository.Add(comment);

            return Task.FromResult(1);
        }
    }
}
