using WebFootballApp.Enum;

namespace WebFootballApp.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Address { get; set; }

    public string Mail { get; set; }

    public string Password { get; set; }

    public DateTime RegistrationDate { get; set; }
    
    public UserRole RoleId { get; set; }
}