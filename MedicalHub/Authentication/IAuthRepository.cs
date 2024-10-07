using MedicalHub.Entities.Users;

namespace MedicalHub.Authentication;

public interface IAuthRepository
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
    string GenerateJwtToken(UserAdmin userAdmin);
}