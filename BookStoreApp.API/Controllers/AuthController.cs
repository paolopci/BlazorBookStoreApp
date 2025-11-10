using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Auth;
using BookStoreApp.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            ILogger<AuthController> logger,
            UserManager<ApiUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterUserDto request)
        {
            _logger.LogInformation("Register request received for {Email}", request.Email);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Register failed: invalid model state for {Email}", request.Email);
                return BadRequest(ModelState);
            }

            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists is not null)
            {
                _logger.LogWarning("Register failed: email already exists for {Email}", request.Email);
                return Conflict("Email già registrata.");
            }

            var user = new ApiUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                _logger.LogWarning("Register failed for {Email}", request.Email);
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            var token = await _tokenService.GenerateTokenAsync(user);
            _logger.LogInformation("Register succeeded for {Email}", request.Email);
            return CreatedAtAction(nameof(Register), token);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request)
        {
            _logger.LogInformation("Login request received for {Email}", request.Email);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login failed: invalid model state for {Email}", request.Email);
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogWarning("Login failed: user not found for {Email}", request.Email);
                return Unauthorized("Credenziali non valide.");
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
            {
                _logger.LogWarning("Login failed: invalid password for {Email}", request.Email);
                return Unauthorized("Credenziali non valide.");
            }

            var token = await _tokenService.GenerateTokenAsync(user);
            _logger.LogInformation("Login succeeded for {Email}", request.Email);
            return Ok(token);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _logger.LogInformation("Logout richiesto per l'utente {User}", User.Identity?.Name ?? "anonimo");
            return Ok("Logout eseguito. Rimuovi il token dal client per completare l'operazione.");
        }
    }
}
