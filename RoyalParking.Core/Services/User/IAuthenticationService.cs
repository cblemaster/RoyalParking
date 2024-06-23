using RoyalParking.Core.DTO;
using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Services.User;

public interface IAuthenticationService
{
    Task<IReturnable> GetUserById(int id);
    Task<IEnumerable<IReturnable>> GetUsers();
    Task<IReturnable> Register(RegisterUserDTO registerUser);

    Task<IReturnable> LogIn(LoginUserDTO logInUser);
    static void LogOut() => UserService.SetLogin(null!);
}
