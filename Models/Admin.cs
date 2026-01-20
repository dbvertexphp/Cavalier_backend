namespace Calavier_backend.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }       // Admin ka name
        public string Email { get; set; }      // Login email
        public string Password { get; set; }   // Login password (plain text for now)
        public string PhoneNumber { get; set; } // Optional phone number
    }
}
