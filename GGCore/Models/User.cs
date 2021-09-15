using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Models
{
    public class User : IdentityUser
    {
    #nullable enable
        public string? About { get; set; }

        public string? ImagePath { get; set; }
    #nullable disable
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Updated { get; set; }

        // M:M relation with Role
        //public ICollection<string> Roles { get; set; }

        // 1:M relation with Comment
        //public IEnumerable<Comment> Comments { get; set; }
    }
}
