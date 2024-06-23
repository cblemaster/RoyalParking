using RoyalParking.Core.DTO;
using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Services.User;

public interface IAuthenticationService
{
    Task<IReturnable> GetUserByIdAsync(int id);
    Task<IEnumerable<IReturnable>> GetUsersAsync();
    Task<IReturnable> RegisterAsync(RegisterUserDTO registerUser);

    Task<IReturnable> LogInAsync(LoginUserDTO logInUser);
    static void LogOut() => UserService.SetLogin(null!);
}
