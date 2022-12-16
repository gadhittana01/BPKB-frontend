using System;
using BPKBP_frontend.Models;

namespace BPKBP_frontend.ViewModels
{
	public class CreateViewModel
	{
		public IEnumerable<Location>? locations { get; set; }
		public BPKB bpkb { get; set; }
	}
}

