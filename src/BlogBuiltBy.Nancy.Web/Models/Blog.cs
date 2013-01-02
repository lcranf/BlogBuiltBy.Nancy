using System;

namespace BlogBuiltBy.Nancy.Web.Models
{
    public class Blog
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}