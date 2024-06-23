namespace RoyalParking.Core.DTO;

public class RegisterUserDTO
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Role { get; set; } = default!;
}
