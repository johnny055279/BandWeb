using System;
namespace webapi.Models
{
	public class DropDown
	{
        public DropDown(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

		public string Name { get; set; }
	}
}

