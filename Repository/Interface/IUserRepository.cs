using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
	public interface IUserRepository
	{
		Account Login(string username, string password);
		Account GetUserByID(Guid id);

		Account GetUserByDisplayName(string displayName);
		List<Account> GetAvailableUsers(string groupName);
	}
}
