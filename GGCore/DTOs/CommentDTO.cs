using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.DTOs
{
    public record CommentDTO
    {
        [Required]
        public string Text { get; set; }

        // 1:M relation with User
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
