using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class File : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileType { get; set; }

        public byte[] FileData { get; set; }

        public DateTime Created { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserFile> UserFiles { get; set; }

        public File()
        {
        }
    }
}
