using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
    private    IBlogRepository _blogRepository;
        public AdminController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        [HttpGet]
        public IActionResult EditPost(int id)
        {            
            return View(_blogRepository.GetPostById(id));
        }
        [HttpPost]
        public IActionResult EditPost(BlogModel post)
        {
           if (ModelState.IsValid)
            {
                _blogRepository.UpdatePost(post);
                return Redirect("~/Blog/Blog");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(BlogModel post)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.CreatePost(post);
                return Redirect("~/Blog/Blog");
            }
            return View();
        }
    }
}