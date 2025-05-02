using System.Diagnostics;
using ContactManagement.Models;
using ContactManagement.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactContext _context;

        public HomeController(ContactContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchEmail)
        {
            IQueryable<Contact> contacts = _context.Contacts;

            if (!string.IsNullOrEmpty(searchEmail))
            {
                contacts = contacts.Where(c => c.Email.Contains(searchEmail));
            }

            return View(contacts.ToList());
        }
    }
}
