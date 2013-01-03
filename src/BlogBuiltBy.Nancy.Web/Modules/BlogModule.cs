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
            Get["/"] = All; 
            Post["/add"] = Add;
            Get["/id/{id}"] = Id;   
        }

        private dynamic Id(dynamic parameters)
        {
            return Response.AsJson(new { IsValid = true, parameters.Id });
        }


        private dynamic All(dynamic parameters)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var blogs = db.Select<Blog>();
                return View["Blogs", blogs];
            }
        }

        private dynamic Add(dynamic parameters)
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