using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.Experiments.Data.Data
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}