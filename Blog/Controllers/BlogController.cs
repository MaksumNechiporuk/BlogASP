using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Interfaces;
using Blog.Models;
using Blog.Repository;
using Blog.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _postRepository;
        private IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;
        public BlogController(IBlogRepository postRepository,IHostingEnvironment hostingEnvironment, ILogger<BlogController> logger)
        {
            _postRepository = postRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;

        }
        [Route("Blog/Post/{id}")]
        [AllowAnonymous]
        public IActionResult Post(int id)
        {
            ViewBag.Title = "Post";
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            var post = _postRepository.GetPostById(id);
            if(post==null)
            {
                Response.StatusCode = 404;
                return View("PostNotFound", id);
            }
            return View(post);
        }
        [AllowAnonymous]
        public IActionResult Blog()
        {
            ViewBag.Title = "Blog";          
            return View(_postRepository.GetAllPosts());
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Blog(PostSearchViewModel post)
        {
            ViewBag.Title = "Blog";
            var list = _postRepository.GetPostByName(post.Name);
            if(list.Count>0)
            return View(list);
            else
            return new StatusCodeResult(404);
        }
        public IActionResult Delete(int id)
        {
            var post = _postRepository.GetPostById(id);
            if (post.Img != null&&post.Img != "news.svg")
            {
               
                string filePath = Path.Combine(hostingEnvironment.WebRootPath,"img", post.Img);
                System.IO.File.Delete(filePath);
            }
            _postRepository.DeletePost(id);
            return RedirectToAction("Blog");

        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            BlogModel post = _postRepository.GetPostById(id);
            PostEditViewModel postEditViewModel = new PostEditViewModel
            {
                Id = post.Id,
                Name = post.Name,
                Author = post.Author,
                PrewText = post.PrewText,
                FullText = post.FullText,
                ExistImgPath = post.Img
            };
            return View(postEditViewModel);
        }
      
        [HttpPost]
        public IActionResult Edit(PostEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                BlogModel post = _postRepository.GetPostById(model.Id);
                post.Author = model.Author;
                post.Name = model.Name;
                post.PrewText = model.PrewText;
                post.FullText = model.FullText;
                if (model.Img != null)
                {
                    if (model.ExistImgPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "img", model.ExistImgPath);
                        System.IO.File.Delete(filePath);
                    }
                    post.Img = UploadedFile(model);
                }

                _postRepository.UpdatePost(post);
                return RedirectToAction("Blog");
            }
            return View();
        }
        private string UploadedFile(PostEditViewModel model)
        {
            string uniqFileName = null;
            if (model.Img != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                uniqFileName = Guid.NewGuid().ToString() + "_" + model.Img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Img.CopyTo(fileStream);
                }
            }

            return uniqFileName;
        }
    }
}