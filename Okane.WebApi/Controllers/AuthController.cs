using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okane.Contracts;
using Okane.Core.Services;

namespace Okane.WebApi.Controllers;

[AllowAnonymous]
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
    public ActionResult<string> CreateToken(SignInRequest request) => 
        Ok(new { Token = _service.GenerateToken(request) });
}