using Blog.Entities;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModel
{
    public class PostSearchViewModel
    {
        public IQueryable<BlogModel> Blog { get; set; }

        public string Text { get; set; }
    }
}
