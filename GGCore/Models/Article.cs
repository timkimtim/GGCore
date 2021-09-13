using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Models
{
    public record Article : Entity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

        // 1:M relation with Section
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
