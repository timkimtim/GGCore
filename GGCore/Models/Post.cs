using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GGCore.Models
{
    public record Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        // 1:M relation with Comment
        [JsonIgnore]
        public IList<Comment> Comments { get; set; }
    }
}
 