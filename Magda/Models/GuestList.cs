using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magda.Models
{
	public class GuestList
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ListId { get; set; }

        public List<Guest> Guests { get; set; }


        public GuestList()
		{
		}
	}
}

