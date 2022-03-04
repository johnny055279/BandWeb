using System;
using System.Collections.Generic;
using webapi.Models;

namespace webapi.DTOs
{
	public class DropDownDto
	{
        public string Type { get; set; }

        public List<DropDown> List { get; set; }
    }
}

