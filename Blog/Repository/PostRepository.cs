using Blog.Entities;
using Blog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class PostRepository : IBlogRepository
    {

        private readonly DBContent context;
        public PostRepository(DBContent context)
        {
            this.context = context;
        }
        public BlogModel CreatePost(BlogModel post)
        {
            post.Date = DateTime.Now;
            context.Blog.Add(post);
            context.SaveChanges();
            return post;
        }

        public BlogModel DeletePost(int Id)
        {
           var post= context.Blog.Find(Id);


      
            if (post!=null)
            {
            context.Blog.Remove(post);
            context.SaveChanges();
            }
            return post;
        }

        public IEnumerable<BlogModel> GetAllPosts()
        {
            var list = context.Blog.ToList();
            list.Reverse();
            return list;
        }

        public BlogModel GetPostById(int id)
        {
            return context.Blog.Find(id);

        }

        public BlogModel UpdatePost(BlogModel newPost)
        {
            var post = context.Blog.Attach(newPost);
            post.State = EntityState.Modified;
            context.SaveChanges();
            return newPost;
        }
    }
}
