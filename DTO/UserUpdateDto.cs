using System.ComponentModel.DataAnnotations;
namespace Calavier_backend.Models
{
    // RegisterDto se sari fields automatically aa jayengi
    public class UserUpdateDto : Calavier_backend.DTO.UserRegisterDto
    {
        [Required]
        public int Id { get; set; } // Update ke liye sirf extra ID chahiye
    }
}