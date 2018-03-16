using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.Experiments.Data
{
    public class Post
    {
        [StringLength(128)]
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<Tag> Tags { get; set; }
    }
}