using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Interfaces;
using Blog.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
    private    IBlogRepository _blogRepository;
        private IHostingEnvironment hostingEnvironment;

        public AdminController(IBlogRepository blogRepository, IHostingEnvironment hostingEnvironment)
        {
            _blogRepository = blogRepository;
            this.hostingEnvironment = hostingEnvironment;

        }
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var post = _blogRepository.GetPostById(id);
            //PostCreateViewModel edit_post = new PostCreateViewModel
            //{
            //    Name = post.Name,
            //    Author = post.Author,
            //    PrewText = post.PrewText,
            //    FullText = post.FullText,
            //    Img = (IFormFile)post.Img
            //};

            return View();
        }
        [HttpPost]
        public IActionResult EditPost(PostCreateViewModel new_post)
        {
            if (ModelState.IsValid)
            {
                string uniqFileName = null;
                if (new_post.Img != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                    uniqFileName = Guid.NewGuid().ToString() + "_" + new_post.Img.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqFileName);
                    new_post.Img.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var post = new BlogModel()
                {
                    Name = new_post.Name,
                    Author = new_post.Author,
                    PrewText = new_post.PrewText,
                    FullText = new_post.FullText,
                    Img = Path.Combine("/", "img", uniqFileName)
                };

                _blogRepository.UpdatePost(post);
                return Redirect($"~/Blog/Post/{post.Id}");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(PostCreateViewModel new_post)
        {
            if (ModelState.IsValid)
            {
                string uniqFileName = null;

                if (new_post.Img!=null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                    uniqFileName = Guid.NewGuid().ToString() + "_" + new_post.Img.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqFileName);
                    new_post.Img.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                else
                {
                    uniqFileName = "news.svg";
                }
                var post = new BlogModel()
                {
                    Name = new_post.Name,
                    Author = new_post.Author,
                    PrewText = new_post.PrewText,
                    FullText = new_post.FullText,
                    Img = Path.Combine("/","img",uniqFileName)
                };
                _blogRepository.CreatePost(post);
                return RedirectToRoute( new { controller = "Blog", action = "Post", Id=post.Id });
            }
            return View();
        }
    }
}