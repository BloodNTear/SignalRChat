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
	public class UserRepository : IUserRepository
	{	
		private DbSet<Account> _dbSet;
		private IChatGroupRepository _chatgroups;
		public UserRepository(SignalRDBContext context, IChatGroupRepository chatGroupRepository)
		{
			_dbSet = context.accounts;
			_chatgroups = chatGroupRepository;
		}

		public List<Account> GetAvailableUsers(string groupName)
		{
			var group = _chatgroups.GetChatGroupByName(groupName);
			return _dbSet.Where( acc => !acc.ChatGroups.Contains(group)).ToList();
		}

		public Account GetUserByDisplayName(string displayName)
		{
			return _dbSet.FirstOrDefault( acc => acc.DisplayName.Equals(displayName));
		}

		public Account GetUserByID(Guid id)
        {
			return _dbSet.FirstOrDefault(acc => acc.UserID.Equals(id));
        }

        public Account Login(string username, string password)
		{
			return _dbSet.Include(acc => acc.Messages).Include(acc => acc.ChatGroups).FirstOrDefault(acc => acc.Username.Equals(username) && acc.Password.Equals(password));
		}
	}
}
