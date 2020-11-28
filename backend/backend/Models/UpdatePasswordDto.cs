using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class UpdatePasswordDto
    {
        public string CurrentPassword { get; set; }

        public string Password { get; set; }
    }
}
