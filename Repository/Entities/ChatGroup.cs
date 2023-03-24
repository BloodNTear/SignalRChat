using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class ChatGroup
	{
		public Guid ChatGroupID { get; set; }
		public string GroupName { get; set; }
		public List<Account> Accounts { get; set; }
		public List<Message> Messages { get; set; }
	}
}
