using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF.Experiments.Data
{
    public class Blog
    {
        public int BlogId;
        [StringLength(128)]
        public string Title;
        public List<Post> Posts { get; set; }
    }
}
