using System.Linq;
using System.Threading.Tasks;
using StellarisPlanetList.Data;
using StellarisPlanetList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NationalParksAcrossAmerica.Controllers
{
    /// <summary>
    /// RegisterViewModel and UserViewModel are the Views in the model
    /// UserAccounts.cs Using this to replace current login and register
    /// buttons.
    /// 
    /// UserAccounts is the final view in UserAccounts Top Public class.
    /// </summary>
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// creates a new users
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // Check if username/email is in use
                bool isEmailTaken = await (from account in _context.Users
                                           where account.Email == reg.Email
                                           select account).AnyAsync();

                // if so, add custom error and send back to view
                if (isEmailTaken)
                {
                    ModelState.AddModelError(nameof(RegisterViewModel.Email), "That emai is already in use");
                }

                bool isUsernameTaken = await (from account in _context.Users
                                              where account.Username == reg.Username
                                              select account).AnyAsync();

                if (isUsernameTaken)
                {
                    ModelState.AddModelError(nameof(RegisterViewModel.Username), "That username is taken");
                }

                if (isEmailTaken || isUsernameTaken)
                {
                    return View(reg);
                }


                UserAccounts acc = new UserAccounts()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    Username = reg.Username
                };


                _context.Users.Add(acc);
                await _context.SaveChangesAsync();

                LogUserIn(acc.UserId);


                return RedirectToAction("Index", "Home");

            }

            return View(reg);
        }

        /// <summary>
        /// if username and password are coreect or exists it logs you in
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {

            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }



            UserAccounts account =
                await (_context.Users
                    .Where(ua => (ua.Username == model.UsernameOrEmail ||
                                ua.Email == model.UsernameOrEmail) &&
                                ua.Password == model.Password)
                .SingleOrDefaultAsync());

            if (account == null)
            {
                ModelState.AddModelError(string.Empty, "Credentials were not found");

                return View(model);
            }
            LogUserIn(account.UserId);

            HttpContext.Session.SetInt32("UserId", account.UserId);

            return RedirectToAction("Index", "Home");
        }

        private void LogUserIn(int accountId)
        {
            HttpContext.Session.SetInt32("UserId", accountId);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}