using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace webapi.Entities
{
	public class News
	{
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

		public string SubTitle { get; set; }

		public string ImageUrl { get; set; }

		public string Content { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

    }
}

