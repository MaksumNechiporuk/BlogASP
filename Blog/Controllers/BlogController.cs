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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _postRepository;
        private IHostingEnvironment hostingEnvironment;

        public BlogController(IBlogRepository postRepository,IHostingEnvironment hostingEnvironment)
        {
            _postRepository = postRepository;
            this.hostingEnvironment = hostingEnvironment;

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
            _postRepository.DeletePost(id);
            var post = _postRepository.GetPostById(id);
            if (post.Img != null)
            {
               
                string filePath = Path.Combine(hostingEnvironment.WebRootPath,"img", post.Img);
                System.IO.File.Delete(filePath);
            }
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