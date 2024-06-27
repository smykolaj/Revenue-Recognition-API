using Project.DTOs;

namespace Project.Services.Interfaces;

public interface ILoginService
{
    void RegisterUser(RegisterRequest model);
    LoginResponseModel Login(LoginRequestModel model);
    string GenerateRefreshToken();
    string GetHashedPasswordWithSalt(string password, string salt);
    Tuple<string, string> GetHashedPasswordAndSalt(string password);

    LoginResponseModel Refresh(string refreshToken);
}