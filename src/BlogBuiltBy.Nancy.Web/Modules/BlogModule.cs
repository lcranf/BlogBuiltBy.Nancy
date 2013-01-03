using System;
using BlogBuiltBy.Nancy.Web.Models;
using Nancy;
using Nancy.ModelBinding;
using ServiceStack.OrmLite;

namespace BlogBuiltBy.Nancy.Web.Modules
{
    public class BlogModule : NancyModule
    {
        private readonly IDbConnectionFactory _dbFactory;

        public BlogModule(IDbConnectionFactory dbFactory)
            : this()
        {
            _dbFactory = dbFactory;
        }

        public BlogModule()
            : base("/blog")
        {
            Get["/"] = ListBlogs; 
            Post["/add"] = AddBlog;
            Put["/update"] = UpdateBlog;
            Post["/delete/{id}"] = DeleteBlog;
            Get["/{id}"] = FindById;   
        }

        private dynamic DeleteBlog(dynamic parameters)
        {
            var blogId = (long) parameters.id;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<Blog>(blogId);
            }

            return Response.AsJson(new
                {
                    IsValid = true,
                    Message = string.Format("Deleted Blog with id of {0}", blogId)
                });
        }

        private dynamic UpdateBlog(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        private dynamic FindById(dynamic parameters)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var blog = db.GetByIdOrDefault<Blog>((long) parameters.id);
                return Response.AsJson(new { IsValid = true, Blog = blog }); 
            }
        }

        private dynamic ListBlogs(dynamic parameters)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var blogs = db.Select<Blog>();
                return View["Blogs", blogs];
            }
        }

        private dynamic AddBlog(dynamic parameters)
        {
            var blog = this.Bind<Blog>();

            blog.CreatedBy = "Unknown User";
            blog.CreatedOn = DateTime.Now;

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(blog);
                blog.Id = db.GetLastInsertId();
            }

            return Response.AsJson(new { IsValid = true, Message = "Added Blog", Blog = blog });
        }
    }
}