using FormManagement.Business;
using FormManagement.DataAccess;
using FormManagement.Entities;
using FormManagementSurface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace FormManagementApp.Surface.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFormService _formService;
        private readonly IUserService _userService;
        private readonly FormDbContext _context;

        public HomeController(IFormService formService, IUserService userService, FormDbContext context)
        {
            _formService = formService;
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Error"] = "Sayfa yüklenirken hata oluþtu. Lütfen tekrar giriþ yapýn.";
                return RedirectToAction("Login");
            }

            var forms = _context.Forms.Include(f => f.User).Where(f => f.CreatedBy == userId.Value).ToList();
            var model = new FormDto
            {
                Forms = forms
            };

            return View(model);
        }

        public IActionResult GetAllForms()
        {
            var forms = _formService.GetAllForms();
            var model = new FormDto
            {
                Forms = forms
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateForm(Form form)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var user = _context.Users.Find(userId.Value);
                form.User = user;
            }
            else
            {
                TempData["Error"] = "Form oluþurken hata oluþtu, tekrar giriþ yapýnýz";
                return View();
            }

            if (form.Fields != null)
            {
                foreach (var field in form.Fields)
                {
                    field.Form = form;
                }
            }

            if (!ModelState.IsValid)
            {
                ModelState.Clear();

                TryValidateModel(form);
            }

            if (ModelState.IsValid)
            {
                _formService.CreateForm(form);
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                TempData["ModelStateErrors"] = errors;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.ValidateUser(username, password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Geçersiz kullanýcý adý veya þifre.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}