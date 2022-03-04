using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
	public class City
	{
        [Key]
        public int Id { get; set; }

        [Required]
		public string CityName { get; set; }
    }
}

