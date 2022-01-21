using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Entities
{
    [Table("PostComment")]
	public class PostComment
	{
        [Key]
        public int Id { get; set; }

        public string Comment { get; set; }

        public Post Post { get; set; }

        public AppUser User { get; set; }
    }
}

