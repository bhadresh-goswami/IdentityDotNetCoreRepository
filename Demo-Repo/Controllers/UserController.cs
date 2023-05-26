using Demo_Repo.Models;
using Microsoft.AspNetCore.Mvc;
using TSB.Repository.IRepository;

namespace Demo_Repo.Controllers
{
    public class UserController : Controller
    {
        IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserVM user)
        {
            var data = await _userRepository.Add(new TSB.Domain.IdentityContext.ApplicationUser()
            {
                Email = user.EmailId,
                UserName = user.UserName,
                PasswordHash = user.Password
            }, "Admin");


            return View();
        }
    }
}
