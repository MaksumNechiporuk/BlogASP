using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Position { get; set; }
    }
}
