using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
	public class News
	{
        public News(string title, string subTitle, string imgUrl, string content)
        {
            Title = title;
            SubTitle = subTitle;
            ImgUrl = imgUrl;
            Content = content;
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

		public string SubTitle { get; set; }

		public string ImgUrl { get; set; }

		public string Content { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

    }
}

