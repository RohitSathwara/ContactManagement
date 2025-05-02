using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models.Domain
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public bool IsFavorite { get; set; }
    }
}
