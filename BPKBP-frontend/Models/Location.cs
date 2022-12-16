using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPKBP_frontend.Models
{
	public class Location
	{
        public string LocationID { get; set; } = string.Empty;

        public string LocationName { get; set; } = string.Empty;
    }
}

