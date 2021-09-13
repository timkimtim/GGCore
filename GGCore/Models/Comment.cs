using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Models
{
    public record Comment : Entity
    {
        public string Text { get; set; }

        // 1:M relation with Post
        public int PostId { get; set; }
        public Post Post { get; set; }

        // 1:M relation with User
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
