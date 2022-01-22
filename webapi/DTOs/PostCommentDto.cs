using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
	public class PostCommentDto
	{
        public PostCommentDto(string comment)
        {
            Comment = comment;
        }

        [Required]
        public string Comment { get; set; }
    }
}

