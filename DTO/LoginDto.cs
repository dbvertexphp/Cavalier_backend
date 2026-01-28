namespace Calavier_backend.DTO
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Iska naam 'SystemType' rakhein kyunki Controller yahi dhoond raha hai
        public string SystemType { get; set; } = "Admin";
    }
}