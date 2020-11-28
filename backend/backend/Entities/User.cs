﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace backend.Entities
{
    public class User : IdentityUser<int>, IEntityBase
    {
        public string Role { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public User()
        {
        }
    }
}
