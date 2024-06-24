using RoyalParking.Core.DTO;

namespace RoyalParking.Core.Services.User;

public static class UserService
{
    private static UserDTO user = null!;

    public static void SetLogin(UserDTO u) => user = u;

    public static int GetUserId() => user is not null ? user.Id : 0;

    public static bool IsLoggedIn() => user is not null && !string.IsNullOrWhiteSpace(user.Token);

    public static string GetToken() => user?.Token ?? string.Empty;
}
