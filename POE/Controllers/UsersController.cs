using DbManagement.Models;
using Microsoft.AspNetCore.Mvc;
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
                Account signUp = new Account();
                await signUp.Register(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
        public async Task<IActionResult> Login([Bind("Username,Password")] string username,string password)
        {
            if (ModelState.IsValid)
            {
                Account login = new Account();
                bool result = login.Login(username, password);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}
