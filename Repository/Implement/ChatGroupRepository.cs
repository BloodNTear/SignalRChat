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
        private DbSet<Account> _users;
        private SignalRDBContext _context;
        public ChatGroupRepository(SignalRDBContext context) {
            _context = context;
            _dbSet = _context.chatGroups;
            _users = _context.accounts;
        }    
        public bool AddChatGroup(ChatGroup chatGroup)
        {
            throw new NotImplementedException();
        }

		public bool AddChatGroup(string groupName)
		{
			ChatGroup newGroup = new ChatGroup();
            
            newGroup.ChatGroupID = Guid.NewGuid();
            newGroup.Accounts = new List<Account>();

            Account admin = _users.FirstOrDefault(acc => acc.Role == Account.AccountRole.Admin);
            
            if (admin != null)
            {
                newGroup.Accounts.Add(admin);
            }

            _dbSet.Add(newGroup);
            
            var rowAffected = _context.SaveChanges();

            return true;
		}

		public bool AddMember(string groupName, string userID)
		{
			ChatGroup target = GetChatGroupByName(groupName);

            Account newUser = GetUserById(Guid.Parse(userID));
            if (newUser != null)
            {
                target.Accounts.Add(newUser);
            }
            _dbSet.Update(target);
            return _context.SaveChanges() > 0;
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

		public bool RemoveMember(string groupName, string UserID)
		{
			ChatGroup target = GetChatGroupByName(groupName);

            Account member = GetUserById(Guid.Parse(UserID));
			if (member != null && target.Accounts.Contains(member))
			{
				target.Accounts.Remove(member);
			}
			_dbSet.Update(target);
			return _context.SaveChanges() > 0;
		}

        private Account GetUserById(Guid userID)
        {
			return _users.FirstOrDefault(acc => acc.UserID.Equals(userID));
		}
	}
}
