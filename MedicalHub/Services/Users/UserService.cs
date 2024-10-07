using MedicalHub.Entities.Users;
using MedicalHub.Infrastructure;
using MedicalHub.Repository;
using Microsoft.EntityFrameworkCore;


namespace MedicalHub.Services.Users;

public class UserService : IUserService
{
    private readonly IRepository<UserAdmin> _repository;
    private readonly AppDbContext _context;
    
    public UserService(IRepository<UserAdmin> repository, AppDbContext context)
    {
        _context = context;
        _repository = repository;
    }

    
    public async Task Add(UserAdmin entity)
    {
        await _repository.Add(entity);
    }

    public Task<UserAdmin?> GetEmailAsync(string email)
    {
        return _context.UserAdmin.FirstOrDefaultAsync(u => u.emailUser == email);
    }
}