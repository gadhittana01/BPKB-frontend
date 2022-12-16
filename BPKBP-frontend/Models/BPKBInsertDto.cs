using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPKBP_frontend.Models
{
	public class BPKBInsertDto
    {
        [Required(ErrorMessage = "Agreement Number mustn't be empty")]
        public string AgreementNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "BPKB no mustn't be empty")]
        public string BPKBNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Branch ID musn't be empty")]
        public string BranchID { get; set; } = string.Empty;

        [Required(ErrorMessage = "BPKB Date musn't be empty")]
        public DateTime BPKBDate { get; set; }

        [Required(ErrorMessage = "Faktur No musn't be empty")]        
        public string FakturNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Faktur Date musn't be empty")]
        public DateTime FakturDate { get; set; }

        [Required(ErrorMessage = "Location musn't be empty")]
        public string LocationFK { get; set; } = string.Empty;

        [Required(ErrorMessage = "Police No musn't be empty")]
        public string PoliceNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "BPKB Date In No musn't be empty")]
        public DateTime BPKBDateIn { get; set; }
    }
}

