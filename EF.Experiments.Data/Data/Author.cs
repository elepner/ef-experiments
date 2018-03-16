using System.Collections.Generic;

namespace EF.Experiments.Data.Data
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Post> Posts { get; set; }
    }
}