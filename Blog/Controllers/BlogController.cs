using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        DBContent content;


        public BlogController(DBContent _content)
        {
            content = _content;
        }
        [Route("Blog/Post/{id}")]
        public IActionResult Post(int id)
        {
            ViewBag.Title = "Post";
            var query = content.Blog.AsQueryable();

            var blogs = query.Where(post=>post.Id==id).Select(p => new BlogViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Author = p.Author,
                FullText = p.FullText,
                Img = p.Img,
                PrewText = p.PrewText
            }).SingleOrDefault();
            return View(blogs);
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