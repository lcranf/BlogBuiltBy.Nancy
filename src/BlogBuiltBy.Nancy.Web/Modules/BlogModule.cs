using Nancy;

namespace BlogBuiltBy.Nancy.Web.Modules
{
    public class BlogModule : NancyModule
    {
        public BlogModule()
            : base("/blog")
        {
            Get["/"] = parameters => "Hello world";
        }
    }
}