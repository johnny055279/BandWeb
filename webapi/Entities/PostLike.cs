using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
	public class PostLike
	{
        public PostLike(int postId, int userId)
        {
            PostId = postId;
            UserId = userId;
        }

        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public AppUser User { get; set; }

        public Post Post { get; set; }
    }
}

