using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Interfaces
{
    public interface IBlog
    {
        IEnumerable<tblBlog> Blogs { get; }
    }
}
