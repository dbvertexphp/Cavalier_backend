using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required] // ✅ Ab Swagger me bhi dikhega
    public string Access { get; set; }
}
