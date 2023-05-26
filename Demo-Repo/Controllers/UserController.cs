using Demo_Repo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TSB.Domain.IdentityContext;
using TSB.Repository.IRepository;

namespace Demo_Repo.Controllers
{
    public class UserController : Controller
    {
        IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
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

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserVM user)
        {
            var resultUser =  await _userRepository.SignIn(new TSB.Domain.IdentityContext.ApplicationUser()
            {
                Email = user.EmailId,
                PasswordHash = user.Password
            });

            if (resultUser != null)
            {
                //email id is valid
                var signInResult = await _signInManager.PasswordSignInAsync(resultUser, resultUser.PasswordHash, isPersistent: false, lockoutOnFailure: false);
                if (signInResult.Succeeded)
                {
                    //sign in done
                    var userR = User;
                }
            }
            return View();
        }
    }
}
