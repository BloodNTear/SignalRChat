using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Repository.Interface;

namespace SinalRAssignment.Pages
{
    public class LoginModel : PageModel
    {
        private IUserRepository _userRepository;

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string username, string password)
        {
            var result = _userRepository.Login(username, password);
            
            if (result == null)
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("UserID", result.UserID.ToString());
                return RedirectToPage("Dashboard");
            }
            
        }
    }
}
