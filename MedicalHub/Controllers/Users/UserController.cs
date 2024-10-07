using MedicalHub.Authentication;
using MedicalHub.Entities.Users;
using MedicalHub.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHub.Controllers.Users;
[ApiController]
[Route("/api/login")] 
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthRepository _authRepository;

    public UserController(UserService userService, AuthRepository authRepository)
    {
        _userService = userService;
        _authRepository = authRepository;
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> RegisterUser(RegisterUser registerUser)
    {
        var existUser = await _userService.GetEmailAsync(registerUser.emailUser);
        

        if (existUser is not null)
        {
            return NotFound("User already exists");
        }
        
        var hashedPassword = _authRepository.HashPassword(registerUser.passwordUser);
        
        var user = UserAdmin.Create(
            registerUser.userName,
            hashedPassword,
            registerUser.emailUser
        );
        
        await _userService.Add(user);
        return Ok(user.Id);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LogIn(LoginUser loginUser)
    {
        Console.WriteLine(loginUser.emailUser);
        var existUser = await _userService.GetEmailAsync(loginUser.emailUser);
    
        if(existUser is null)
        {
            return NotFound();
        }
        
        if(!_authRepository.VerifyPassword(existUser.passwordUser, loginUser.passwordUser))
        {
            return BadRequest();
        }
        
        string token = _authRepository.GenerateJwtToken(existUser);
        var response = new { token };
    
        return Ok(response);
    }
}