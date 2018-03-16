using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.Experiments.Data.Data
{
    public class Blog
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        public DateTime Created { get; set; }
        public List<Post> Posts { get; set; }
    }
}
