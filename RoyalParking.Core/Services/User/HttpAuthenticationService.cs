using RoyalParking.Core.DTO;
using RoyalParking.Core.Interfaces;
using RoyalParking.Core.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RoyalParking.Core.Services.User;

public class HttpAuthenticationService : HttpServiceBase, IAuthenticationService
{
    public async Task<IReturnable> GetUserByIdAsync(int id)
    {
        SetRequestHeaders();

        try
        {
            HttpResponseMessage response = await _client.GetAsync($"/user/{id}");

            return response.IsSuccessStatusCode && response.Content is not null
                ? await response.Content.ReadFromJsonAsync<UserDTO>() as IReturnable
                    ?? new NotFound() { Message = $"User with id {id} not found." }
                : new NotFound() { Message = $"User with id {id} not found." };
        }
        catch (Exception) { throw; }
    }

    public async Task<IEnumerable<IReturnable>> GetUsersAsync()
    {
        SetRequestHeaders();

        try
        {
            HttpResponseMessage response = await _client.GetAsync($"/user");

            return response.IsSuccessStatusCode && response.Content is not null
                ? (IEnumerable<IReturnable>)response.Content.ReadFromJsonAsAsyncEnumerable<UserDTO>()
                : new List<UserDTO>().AsEnumerable();
        }
        catch (Exception) { throw; }
    }

    public async Task<IReturnable> RegisterAsync(RegisterUserDTO registerUser)
    {
        StringContent content = new(JsonSerializer.Serialize(registerUser));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PostAsync($"/user/register", content);
            return !response.IsSuccessStatusCode
                ? response.StatusCode == System.Net.HttpStatusCode.BadRequest
                    ? new BadRequest() { Message = await response.Content.ReadAsStringAsync() ?? string.Empty }
                    : new Error() { Message = "An unknown error occured" }
                : response.Content is not null
                ? await response.Content.ReadFromJsonAsync<UserDTO>() as IReturnable
                    ?? new Error() { Message = "An unknown error occured" }
                : new Error() { Message = "An unknown error occured" };
        }
        catch (Exception) { throw; }
    }

    public async Task<IReturnable> LogInAsync(LoginUserDTO logInUser)
    {
        StringContent content = new(JsonSerializer.Serialize(logInUser));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PostAsync($"/user/login", content);
            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode == System.Net.HttpStatusCode.BadRequest
                    ? new BadRequest() { Message = await response.Content.ReadAsStringAsync() ?? string.Empty }
                    : response.StatusCode == System.Net.HttpStatusCode.Unauthorized
                        ? new Unauthorized() { Message = "Login failed." }
                        : new Error() { Message = "An unknown error occured" };
            }
            else
            {
                if (response.Content is not null)
                {
                    UserDTO dto = (await response.Content.ReadFromJsonAsync<UserDTO>())!;
                    if (dto is not null)
                    {
                        UserService.SetLogin(dto);
                        return dto;
                    }                    
                }

                return new Error() { Message = "An unknown error occured" };
            }
        }
        catch (Exception) { throw; }
    }

    public static void LogOut() => UserService.SetLogin(null!);

    private void SetRequestHeaders() => _client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", UserService.GetToken());
}
