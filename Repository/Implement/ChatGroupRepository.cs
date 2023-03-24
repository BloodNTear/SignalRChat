using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class ChatGroupRepository : IChatGroupRepository
    {
        private DbSet<ChatGroup> _dbSet;
        public ChatGroupRepository(SignalRDBContext context) {
            _dbSet = context.chatGroups;
        }    
        public bool AddChatGroup(ChatGroup chatGroup)
        {
            throw new NotImplementedException();
        }

		public bool AddChatGroup(string groupName)
		{
			throw new NotImplementedException();
		}

		public bool AddMember(ChatGroup group, Account member)
        {
            throw new NotImplementedException();
        }

        public List<ChatGroup> GetAllGroups(Account user)
        {
            List<ChatGroup> chatGroups = new List<ChatGroup>();

            foreach(var group in _dbSet.Include(cg => cg.Messages).Include(cg => cg.Accounts).Where(cg => cg.Accounts.Contains(user)))
            {
                chatGroups.Add(group);
            }

            return chatGroups;
        }

		public List<Account> GetAllUsers(string groupName)
		{
			var group = _dbSet.Include(group => group.Accounts).FirstOrDefault(group => group.GroupName.Equals(groupName));

            group.Accounts.RemoveAll(acc => acc.Role == Account.AccountRole.Admin);
            
            return group.Accounts.ToList();
		}

		public ChatGroup GetChatGroupByName(string name)
		{
            return _dbSet.Include(group => group.Messages).Include(group => group.Accounts).FirstOrDefault(group => group.GroupName.Equals(name));
		}

		public bool RemoveMember(Account admin, ChatGroup group, Account member)
        {
            throw new NotImplementedException();
        }
    }
}
