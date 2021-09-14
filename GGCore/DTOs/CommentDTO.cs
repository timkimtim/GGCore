using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.DTOs
{
    public record CommentDTO : EntityDTO
    {
        [Required]
        public string Text { get; set; }
        public PostDTO Post { get; set; }

        // 1:M relation with User
        //public int UserId { get; set; }
        //public User User { get; set; }
    }

    public record CreateCommentDTO : CreateEntityDTO
    {
        [Required]
        public string Text { get; set; }
        public int PostId { get; set; }

        // 1:M relation with User
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
