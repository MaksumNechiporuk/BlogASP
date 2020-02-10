using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Entities
{
    public class DBContent : DbContext
    {
        public DBContent(DbContextOptions<DBContent> options) : base(options)
        {

        }
        public DbSet<BlogModel> Blog { get; set; }

    }
}
