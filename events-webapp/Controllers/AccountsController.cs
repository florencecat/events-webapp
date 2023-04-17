using wa_dev_coursework.Models.EventsContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using wa_dev_coursework.ViewModels;

namespace wa_dev_coursework.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        // Attributes
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // Constructor
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Methods
        [HttpPost]
        [Route("api/account/registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody] RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Добавление нового пользователя
                User user = new() { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                // Регистрация роли нового пользователя
                await _userManager.AddToRoleAsync(user, "user");

                // Установка cookie
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok(new { message = "Добавлен новый пользователь: " + user.UserName });
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };

                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };

                return Created("", errorMsg);
            }
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberUser, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    IList<string>? roles = await _userManager.GetRolesAsync(user);
                    string? userRole = roles.FirstOrDefault();

                    return Ok(new { message = "Выполнен вход", userName = model.Username, userRole });
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };

                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };

                return Created("", errorMsg);
            }
        }

        [HttpPost]
        [Route("api/account/logoff")]
        public async Task<IActionResult> LogOff()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null) return Unauthorized(new { message = "Сначала выполните вход" });

            await _signInManager.SignOutAsync(); // Удаление куки
            return Ok(new { message = "Выполнен выход", userName = usr.UserName });
        }

        [HttpGet]
        [Route("api/account/isauthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            User user = await GetCurrentUserAsync();
            if (user == null) return Unauthorized(new { message = "Вы Гость. Пожалуйста, выполните вход" });

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string? role = roles.FirstOrDefault();

            return Ok(new { message = "Сессия активна", userName = user.UserName, role });
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}