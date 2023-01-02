using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magda.Models
{
	public class Order
	{
		
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? OrderId { get; set; }
		public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
		public enum EventType 
		{
			PublicEvent,
			PrivateEvent,
		}
		public EventType OrderType { get; set; }
        public string RoomName { get; set; }
        public string AdditionalRemarks { get; set; }
		public int Status { get; set; }
		public string? ListId { get; set; }
        public GuestList? GuestList { get; set; }

        public Order()
		{
		}
	}
}

