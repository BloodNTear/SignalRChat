using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Repository.Entities;
using Repository.Interface;

namespace SinalRAssignment.Pages
{
    public class DashboardModel : PageModel
    {
        private IChatGroupRepository _chatGroups;
        private IMessageRepository _messages;
        private IUserRepository _users;

        public Account User { get; set; }

        public DashboardModel(IChatGroupRepository chatGroupRepository, IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _chatGroups = chatGroupRepository;
            _messages = messageRepository;
            _users = userRepository;      
        }
        public IActionResult OnGet()
        {
            try
            {
				Guid userID = Guid.Parse(HttpContext.Session.GetString("UserID"));
				User = _users.GetUserByID(userID);
                return Page();
			}catch(Exception ex)
            {
                return RedirectToPage("Unauthorized");
            }
        }

        public IActionResult OnGetUsersGroups()
        {
            Guid userID = Guid.Parse(HttpContext.Session.GetString("UserID"));
            User = _users.GetUserByID(userID);

            bool status = true;
            string errorMessage = string.Empty;
            List<ChatGroup> chatGroups = new List<ChatGroup>();
            try
            {
                chatGroups = _chatGroups.GetAllGroups(User);
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }

            var data = JsonConvert.SerializeObject(chatGroups, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage,
                data = data
            });
        }

        public IActionResult OnGetGroupMessages(string groupName)
        {
			bool status = true;
			string errorMessage = string.Empty;

			var chatgroup = _chatGroups.GetChatGroupByName(groupName);
            var messages = chatgroup.Messages;

			var data = JsonConvert.SerializeObject(messages, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});

			return new JsonResult(new
			{
				status = status,
				errorMessage = errorMessage,
				data = data
			});
		}

        public JsonResult OnGetAvailableUsers(string groupName)
        {
            bool status = true;
            string errorMessage = string.Empty;

            List<Account> accounts = _users.GetAvailableUsers(groupName);

			var data = JsonConvert.SerializeObject(accounts, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});

            return new JsonResult(new {
				status = status,
				errorMessage = errorMessage,
				data = data
			});
		}

		public JsonResult OnGetGroupUsers(string groupName)
		{
			bool status = true;
			string errorMessage = string.Empty;

            List<Account> accounts = _chatGroups.GetAllUsers(groupName);

			var data = JsonConvert.SerializeObject(accounts, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});

			return new JsonResult(new
			{
				status = status,
				errorMessage = errorMessage,
				data = data
			});
		}

        public JsonResult OnGetAddUser(string groupName, string userId)
        {
			bool status = true;
			string errorMessage = string.Empty;

			return new JsonResult(new
			{
				status = status,
				errorMessage = errorMessage
			});
		}
        public JsonResult OnGetRemoveUser(string groupName, string userId)
        {
			bool status = true;
			string errorMessage = string.Empty;

			return new JsonResult(new
			{
				status = status,
				errorMessage = errorMessage
			});
		}
	}
}

