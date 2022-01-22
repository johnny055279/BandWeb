using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Entities
{
    [Table("PostComment")]
	public class PostComment
	{
        public PostComment(int postId, int userId, string comment)
        {
            PostId = postId;
            UserId = userId;
            Comment = comment;
        }

        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }

        public DateTime CommentTime { get; set; } = DateTime.Now;

        public Post Post { get; set; }

        public AppUser User { get; set; }
    }
}

