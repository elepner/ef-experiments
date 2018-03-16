using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Experiments.ViewModels
{
    class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Posts { get; set; }
    }
}
