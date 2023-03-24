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
		bool AddMember(ChatGroup group, Account member);
		bool RemoveMember(Account admin, ChatGroup group, Account member);
		bool AddChatGroup(ChatGroup chatGroup);
		List<ChatGroup> GetAllGroups(Account user);
		ChatGroup GetChatGroupByName(string name);
		bool AddChatGroup(string groupName);
		List<Account> GetAllUsers(string groupName);
	}
}
