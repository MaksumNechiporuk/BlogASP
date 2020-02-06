using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        DBContent content;


        public BlogController(DBContent _content)
        {
            content = _content;
        }
        public IActionResult Blog()
        {
            ViewBag.Title = "Blog";
            var query = content.Blog.AsQueryable();

            var blogs = query.Select(p => new BlogViewModel
            {
                Id = p.Id,
                Name = p.Name,
               Author=p.Author,
               FullText=p.FullText,
               Img=p.Img,
               PrewText=p.PrewText
               

            }).ToList();          
            return View(blogs);
        }
    }
}