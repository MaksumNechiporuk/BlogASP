using Blog.Entities;
using Blog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BlogRepository : IBlog
    {
        private readonly DBContent dbContent;
        public BlogRepository(DBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        public IEnumerable<tblBlog> Blogs => dbContent.Blog;
    }
}