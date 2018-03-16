using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Experiments.Data.Data
{
    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
