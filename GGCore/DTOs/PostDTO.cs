using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.DTOs
{
    public record CreatePostDTO
    {
        [Required]
        [StringLength(60, ErrorMessage = "Длина {0} превышает допустимое значение ({1} символов).")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }

    public record UpdatePostDTO : CreatePostDTO
    {
    }

    public record PostDTO : CreatePostDTO
    {
        public IList<CommentDTO> Comments { get; set; }
    }
}
