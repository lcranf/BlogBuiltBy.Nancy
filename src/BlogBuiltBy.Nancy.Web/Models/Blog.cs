using System;
using ServiceStack.DataAnnotations;

namespace BlogBuiltBy.Nancy.Web.Models
{
    public class Blog
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}