using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.Experiments.Data.Data
{
    public class Tag
    {
        [StringLength(64)]
        public string Id { get; set; }
        public string Description { get; set; }
        public List<PostTag> PostTags{ get; set; }
    }
}