using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Models
{
    public record Section : Entity
    {
        public string Title { get; set; }
        public string Slug { get; set; }

        // M:1 relation with Article
        public IEnumerable<Article> Articles { get; set; }
    }
}
