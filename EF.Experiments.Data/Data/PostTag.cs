using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF.Experiments.Data.Data
{
    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        [StringLength(64)]
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
