using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Interfaces;
using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _postRepository;

        public BlogController(IBlogRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [Route("Blog/Post/{id}")]
        public IActionResult Post(int id)
        {
            ViewBag.Title = "Post";
            return View(_postRepository.GetPostById(id));
        }
        public IActionResult Blog()
        {
            ViewBag.Title = "Blog";          
            return View(_postRepository.GetAllPosts());
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Blog";
            _postRepository.DeletePost(id);
            return Redirect("~/Blog/Blog");

        }
    }
}