using Microsoft.AspNetCore.Mvc;
using Okane.Contracts;
using Okane.Core.Services;

namespace Okane.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service) => 
        _service = service;

    [HttpPost("signup")]
    public UserResponse SignUp(SignUpRequest request) => 
        _service.SignUp(request);
    
    [HttpPost("token")]
    public string CreateToken(SignInRequest request) => 
        _service.GenerateToken(request);
}