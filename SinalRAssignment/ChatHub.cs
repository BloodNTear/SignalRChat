using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using Repository.Entities;
using Repository.Interface;

namespace SinalRAssignment
{
	public class ChatHub : Hub
	{
		private IUserRepository _users;
		private IChatGroupRepository _chatGroupRepository;
		private IMessageRepository _messageRepository;

		public ChatHub(IUserRepository users, IChatGroupRepository chatGroupRepository, IMessageRepository messageRepository)
		{
			_users = users;
			_chatGroupRepository = chatGroupRepository;
			_messageRepository = messageRepository;
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public async Task SendGroupMessage(string user, string message, string groupName)
		{
			var User = _users.GetUserByDisplayName(user);
			var chatGroup = _chatGroupRepository.GetChatGroupByName(groupName);
			_messageRepository.SendMessage(message, User, chatGroup);
			await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
		}

		public async Task AddMember(string groupName, string memberName)
		{
			await Clients.All.SendAsync("NewMemberJoined", memberName);
		}

		public async Task RemoveMember(string groupName, string memberName)
		{
			await Clients.All.SendAsync("MemberRemoved", memberName);
		}

		public async Task LeaveGroup(string groupName)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
		}

		public async Task JoinGroup(string user, string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		}
	}
}
