using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Entities
{
    public class DBContent : IdentityDbContext<AppUser>
    {
        public DBContent(DbContextOptions<DBContent> options) : base(options)
        {

        }
        public DBContent()
        {

        }
        public DbSet<BlogModel> Blog { get; set; }

    }
}
