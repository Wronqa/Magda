using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magda.Models
{
	public class Guest
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? GuestId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		public string? AdditionalRemarks { get; set; }

		
        public string? ListId { get; set; }

        public GuestList? GuestList { get; set; }
		

		public Guest()
		{
		}
	}
}

