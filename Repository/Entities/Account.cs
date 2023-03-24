using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Account
	{
		[Key]
		public Guid UserID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string DisplayName { get; set; }
		public List<Message> Messages{ get; set; }
		public List<ChatGroup> ChatGroups { get; set; }
		public AccountRole Role { get; set; }

		public enum AccountRole
		{
			Admin,
			Member
		}
	}
}
