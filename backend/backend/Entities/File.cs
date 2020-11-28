using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class File : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileUrl { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public File()
        {
        }
    }
}
