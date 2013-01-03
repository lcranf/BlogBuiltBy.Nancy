using System.Web;
using BlogBuiltBy.Nancy.Web.Models;
using Nancy.Hosting.Aspnet;
using ServiceStack.OrmLite;

namespace BlogBuiltBy.Nancy.Web.Infrastructure
{
    public class WebBootstrapper : DefaultNancyAspNetBootstrapper
    {
        protected override void ConfigureApplicationContainer(global::Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var appDataPath = HttpContext.Current.Server.MapPath("~/App_Data/blogdb.sqlite");
            var dbFactory = new OrmLiteConnectionFactory(appDataPath, SqliteDialect.Provider);

            CreateDatabase(dbFactory);
            container.Register<IDbConnectionFactory>(dbFactory);
        }

        private void CreateDatabase(OrmLiteConnectionFactory dbFactory)
        {
            using (var db = dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<Blog>();
            }
        }
    }
}