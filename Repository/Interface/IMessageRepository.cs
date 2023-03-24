using Repository.Entities;

namespace Repository.Interface
{
	public interface IMessageRepository
	{
		bool SendMessage(string message, Account from, ChatGroup to);
	}
}
