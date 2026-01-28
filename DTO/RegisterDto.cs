namespace Calavier_backend.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
    }
}