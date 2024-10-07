using MedicalHub.Abstractions;

namespace MedicalHub.Entities.Users;

public class UserAdmin : Entity
{
    private UserAdmin(Guid id, string nameUser, string passwordUser, string emailUser) : base(id)
    {
        this.nameUser = nameUser;
        this.passwordUser = passwordUser;
        this.emailUser = emailUser;
    }
    public string nameUser { get; set; }
    public string passwordUser { get; set; }
    public string emailUser { get; set; }  
    
    public static UserAdmin Create(string nameUser, string passwordUser, string emailUser)
    {
        var user = new UserAdmin(
            Guid.NewGuid(),
            nameUser,
            passwordUser,
            emailUser
        );

        return user;
    }
}