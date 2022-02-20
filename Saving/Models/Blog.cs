using System.Collections.Generic;

namespace Saving.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }
        public override string ToString() => BlogId.ToString();

    }
}