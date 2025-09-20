using Microsoft.AspNetCore.Mvc;
using PetsMobile.Services.Interface;

[Route("api/v3/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

   
}