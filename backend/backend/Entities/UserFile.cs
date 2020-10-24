using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Entities
{
    public class UserFile : IEntityBase
    {
        public int Id { get; set; }

        public bool Creator { get; set; }

        public bool Downloaded { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int FileId { get; set; }

        public File File { get; set; }

        public UserFile()
        {

        }
    }
}
