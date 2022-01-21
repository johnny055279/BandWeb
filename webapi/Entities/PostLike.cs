using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
	public class PostLike
	{
		[Key]
        public int Id { get; set; }

        public AppUser User { get; set; }

        public Post Post { get; set; }
    }
}

