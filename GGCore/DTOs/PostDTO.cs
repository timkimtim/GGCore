using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.DTOs
{
    public record CreatePostDTO : CreateEntityDTO
    {
        [Required]
        [StringLength(60, ErrorMessage = "Длина {0} превышает допустимое значение ({1} символов).")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }

    public record PostDTO : EntityDTO
    {
        [Required]
        [StringLength(60, ErrorMessage = "Длина {0} превышает допустимое значение ({1} символов).")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public IList<CommentDTO> Comments { get; set; }
    }
}
