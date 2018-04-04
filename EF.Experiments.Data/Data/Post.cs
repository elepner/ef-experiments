using System;
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

        public int? Rating { get; set; }

        public DateTime Created { get; set; }
        public List<PostTag> PostTags { get; set; }

        public override string ToString()
        {
            return $"PostId: ${Id}; Title: {Title}; AuthorId ${AuthorId}";
        }
    }
}