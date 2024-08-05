using EmployeeHR.ViewModels;
using EmployeeHR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHR.Controllers
{

    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    //UserName=new MailAddress(model.Email).User,
                    UserName = model.UserName,
                    Email = model.Email
                };
                var response = await _userManager.CreateAsync(user, model.Password);
                //for update user info:
                //var r2 = await _userManager.UpdateAsync(user);
                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _userManager.AddToRoleAsync(user, "Office");
                    return RedirectToAction("Login", "Account");
                }
                foreach (var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);

                }
                return View(model);
            }
            return View(model);
        }



        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
            return View(model);
        }
        #endregion

        #region Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //return RedirectToAction("Login");
            return RedirectToAction("Index", "Home");

        }
        #endregion

        #region Manage
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            //var user = await _userManager.FindByNameAsync(currentUser.UserName);
            if (currentUser != null)
            {
                var viewModel = new ManageUserViewModel
                {
                    Username = currentUser.UserName,
                    PhoneNumber = currentUser.PhoneNumber
                };
                return View(viewModel);
            }
            //ModelState.AddModelError("", "");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null)
                {
                    currentUser.PhoneNumber = model.PhoneNumber; 
                    await _userManager.UpdateAsync(currentUser);
                }

            }
            return RedirectToAction("Index", "Home");

        }

        #endregion

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }

    }
}
