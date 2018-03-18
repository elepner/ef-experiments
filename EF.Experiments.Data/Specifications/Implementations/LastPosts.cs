using System;
using System.Collections.Generic;
using System.Text;
using EF.Experiments.Data.Data;

namespace EF.Experiments.Data.Specifications.Implementations
{
    public class LastPosts : Specification<Post>
    {
        LastPosts(int days) : base(post => post.Created > DateTime.Now.AddDays(-days)) { }
    }

    public class ByAuthor : Specification<Post>
    {
        ByAuthor(Author author) : base(post => post.AuthorId == author.Id) { }
    }
}
