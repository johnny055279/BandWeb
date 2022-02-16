using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
	public class City
	{
        public City(string cityName)
        {
            CityName = cityName;
        }

        [Key]
        public int Id { get; set; }

        [Required]
		public string CityName { get; set; }

        public Ticket Ticket { get; set; }
    }
}

