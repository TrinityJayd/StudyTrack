using DbManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules;

namespace POE.Controllers
{
    public class UsersController : Controller
    {

        private readonly Prog6212P2Context _context;

        public UsersController(Prog6212P2Context context)
        {
            _context = context;
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Lastname,Username,Password,CellNumber,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account();
                ValidationMethods validation = new ValidationMethods();
                //Validation for the sign up form
                if (!validation.OnlyLetters(user.Name))
                {
                    ModelState.AddModelError("Name", "Name must only contain letters.");

                }
                //check if the last name is valid
                else if (!validation.OnlyLetters(user.Lastname))
                {
                    ModelState.AddModelError("Lastname", "Last name must only contain letters.");

                }
                else if (account.UsernameExists(user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");

                }
                else if (!validation.PasswordRequirements(user.Password))
                {
                    //Code Attribution
                    //Author:CodeNotFound
                    //Link:https://stackoverflow.com/questions/43561214/display-error-message-on-the-view-from-controller-asp-net-mvc-5
                    ModelState.AddModelError("Password", "Password must contain at least 8 characters, 1 uppercase, 1 lowercase, 1 number and 1 special character.");
                }
                else if (!validation.IsPhoneNumberValid(user.CellNumber))
                {
                    ModelState.AddModelError("CellNumber", "Cell number must be numeric and begin with 0.");

                }
                else if (account.PhoneExists(user.CellNumber))
                {
                    ModelState.AddModelError("CellNumber", "User with this cell number already exists.");
                }
                else if (account.EmailExists(user.Email))
                {
                    ModelState.AddModelError("Email", "User with this email already exists.");

                }
                else
                {
                    //register the user
                    await account.Register(user);
                    return RedirectToAction("Login", "Users");
                }

                return View();
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] string username, string password)
        {
            if (ModelState.IsValid)
            {
                Account login = new Account();
                //get the user's login result
                int result = login.Login(username, password);
                if (result > 0)
                {
                    // if the result is larger than 0, the user is logged in
                    HttpContext.Session.SetInt32("UserID", result);
                    ViewData["UserID"] = result;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //otherwise they have failed
                    ModelState.AddModelError("Password", "Username or password is incorrect.");
                    return View();
                }

            }
            return View();
        }

        public IActionResult Logout()
        {
            //logout the user redirect them to the home page
            HttpContext.Session.SetInt32("UserID", 0);
            ViewData["UserID"] = 0;
            return RedirectToAction("Index", "Home");
        }

    }
}
