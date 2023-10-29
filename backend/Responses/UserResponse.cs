namespace WebFootballApp.Responses;

public class UserResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Address { get; set; }

    public string Mail { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string Role { get; set; }
}