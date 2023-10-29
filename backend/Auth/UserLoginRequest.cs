using System.ComponentModel.DataAnnotations;

namespace WebFootballApp.Auth;

public class UserLoginRequest
{
    [Required]
    public string Mail { get; set; }
    
    [Required]
    public string Password { get; set; }
}