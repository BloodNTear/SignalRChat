using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Message
	{
		[Key]
		public Guid MessageID { get; set; }
		public Account FromAccount { get; set; }
		public ChatGroup ToChatGroup { get; set; }
		public string MessageContent { get; set; }
		public DateTime MessageTime { get; set; }
	}
}
