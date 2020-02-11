using Blog.Entities;
using Blog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            if(post!=null)
            {
            context.Blog.Remove(post);
            context.SaveChanges();
            }
            return post;
        }

        public IEnumerable<BlogModel> GetAllPosts()
        {
            return context.Blog;
        }

        public BlogModel GetPostById(int id)
        {
            return context.Blog.Find(id);

        }

        public BlogModel UpdatePost(BlogModel newPost)
        {
            //var post = context.Blog.Find(newPost.Id);
            ////post.Img=newPost.Img;
            //post.Name = newPost.Name;
            //post.PrewText = newPost.PrewText;
            //post.FullText = newPost.FullText;
            //post.Author = newPost.Author;
            context.Update(newPost);
            context.SaveChanges();
            return newPost;
        }
    }
}
