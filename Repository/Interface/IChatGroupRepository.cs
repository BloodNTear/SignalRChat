using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IChatGroupRepository
	{
		bool AddMember(string groupName, string userID);
		bool RemoveMember(string groupName, string UserID);
		bool AddChatGroup(ChatGroup chatGroup);
		List<ChatGroup> GetAllGroups(Account user);
		ChatGroup GetChatGroupByName(string name);
		bool AddChatGroup(string groupName);
		List<Account> GetAllUsers(string groupName);
	}
}
