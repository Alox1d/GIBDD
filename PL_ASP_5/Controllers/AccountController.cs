using DAL.Entities;
using DAL_ASP_5.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL_ASP_5.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Inspector> _userManager;
        private readonly SignInManager<Inspector> _signInManager; 
        public AccountController(UserManager<Inspector> userManager,
            SignInManager<Inspector> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody]
        RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Inspector inspector = new Inspector { 
                    Email = model.Email, 
                    UserName = model.Email };
                // Добавление нового сотрудника ДПС 
                var result = await _userManager.CreateAsync(inspector, model.Password);
                if (result.Succeeded)
                {
                    // установка куки 
                    await _signInManager.SignInAsync(inspector, false);
                    var msg = new
                    { message = "Добавлен новый сотрудник ДПС: " + inspector.UserName };
                    return Ok(msg);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    var errorMsg = new
                    {
                        message = "Сотрудник ДПС не добавлен.",
                        error = ModelState.Values.SelectMany(e =>
                        e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные.",
                    error = ModelState.Values.SelectMany(e =>
                    e.Errors.Select(er => er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/Login")]
        //[ValidateAntiForgeryToken] 
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await
 _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var msg = new
                    {
                        message = "Выполнен вход пользователем: " + model.Email
                    };
                    return Ok(msg);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен.",
                        error = ModelState.Values.SelectMany(e =>
e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                    error = ModelState.Values.SelectMany(e =>
e.Errors.Select(er => er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/LogOff")]
        //[ValidateAntiForgeryToken] 
        public async Task<IActionResult> LogOff()
        {
            // Удаление куки 
            await _signInManager.SignOutAsync();
            var msg = new
            {
                message = "Выполнен выход."
            };
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/Account/isAuthenticated")]
        //[ValidateAntiForgeryToken] 
        public async Task<IActionResult> LogisAuthenticatedOff()
        {
            Inspector usr = await GetCurrentUserAsync();
            var message = usr == null ? "Вы Гость. Пожалуйста, выполните вход." : "Вы вошли как: " + usr.UserName;
            var msg = new
            {
                message
            };
            return Ok(msg);

        }
        private Task<Inspector> GetCurrentUserAsync() =>
 _userManager.GetUserAsync(HttpContext.User);

    }
}
