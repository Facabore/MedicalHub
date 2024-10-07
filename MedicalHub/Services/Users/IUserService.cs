using MedicalHub.Entities.Users;

namespace MedicalHub.Services.Users;

public interface IUserService
{
    Task<UserAdmin?> GetEmailAsync(string email);
}