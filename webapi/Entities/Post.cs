using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Entities
{
    [Table("Post")]
    public class Post
	{
        public Post(string title, string content)
        {
            Title = title;
            Content = content;
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; } = 0;

		public DateTime LastEditTime { get; set; } = DateTime.Now;

        public ICollection<PostComment> PostComment { get; set; }

        public ICollection<PostLike> PostLike { get; set; }
    }
}

