using ContactManagement.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactContext _context;

        public ContactController(ContactContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact, string confirmPassword)
        {
            if (_context.Contacts.Any(c => c.Email == contact.Email))
            {
                ModelState.AddModelError("Email", "This email is already registered.");
            }

            if (contact.Password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords do not match.");
            }

            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(int id, Contact contact, string confirmPassword)
        {
            if (id != contact.Id) return NotFound();

            if (contact.Password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Passwords do not match.");
            }

            if (!ModelState.IsValid) return View(contact);

            try
            {
                _context.Update(contact);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                if (!_context.Contacts.Any(c => c.Id == contact.Id)) return NotFound();
                throw;
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            return contact == null ? NotFound() : View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ToggleFavorite(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.IsFavorite = !contact.IsFavorite;
            _context.SaveChanges();

            return Json(new { success = true, isFavorite = contact.IsFavorite }); 
        }

        public async Task<IActionResult> GetFavorites(string searchEmail)
        {
            IQueryable<Contact> favoriteContacts = _context.Contacts.Where(c => c.IsFavorite);

            if (!string.IsNullOrEmpty(searchEmail))
            {
                favoriteContacts = favoriteContacts.Where(c => c.Email.Contains(searchEmail));
            }

            var favoritesList = await favoriteContacts.ToListAsync();
            return View(favoritesList);
        }

    }
}
