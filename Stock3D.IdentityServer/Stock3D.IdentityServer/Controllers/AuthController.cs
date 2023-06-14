using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stock3D.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.IdentityServer.Controllers
{
  public class AuthController: Controller
  {
    //для реализации входа пользователя и поиска
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    //для логаута
    private readonly IIdentityServerInteractionService _interactionService;

    //будем внедрять их через конструктор
    public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService)
    {
      (_signInManager, _userManager, _interactionService) =
            (signInManager, userManager, interactionService);
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
      var viewModel = new LoginViewModel
      {
        ReturnUrl = returnUrl,
      };
      return View(viewModel);
    }
    //куда будет переходить управление из формы логина
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
      //проверяем данные на валидность
      if (!ModelState.IsValid)
      {
        return View(viewModel);
      }

      //ищем пользователя
      var user = await _userManager.FindByNameAsync(viewModel.Username);
      //если пользователь не найден
      if (user == null)
      {
        //добавим сообщение об ошибке в состояние модели
        ModelState.AddModelError(string.Empty, "User not found");
        return View(viewModel);
      }
      //В методе PasswordSignInAsync isPersistent куки это когда мы закрываем браузер а они сохраняются
      //lockout - если несколько неуспешных попыток то блочим акк
      var result = await _signInManager.PasswordSignInAsync(viewModel.Username,
        viewModel.Password, false, false);
      //в случае успеха делаем переход по адресу
      if (result.Succeeded)
      {
        return Redirect(viewModel.ReturnUrl);
      }
      //иначе сообщаем об ошибке
      ModelState.AddModelError(string.Empty, "Login error");
      //будет перенаправлять туда откуда пришел запрос
      return View(viewModel);
    }

    //registration method get - для возврата формы и post для действий после заполнения формы
    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
      var viewModel = new RegisterViewModel
      {
        ReturnUrl = returnUrl,
      };
      return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
      //проверяем состояние модели
      if (!ModelState.IsValid)
      {
        return View(viewModel);
      }

      var user = new AppUser
      {
        UserName = viewModel.Username,
      };

      //создаем пользователя с поощью юзер менеджера
      var result = await _userManager.CreateAsync(user, viewModel.Password);
      if (result.Succeeded)
      {
        //делаем вход с помощью signIn менеджера
        await _signInManager.SignInAsync(user, false);
        return Redirect(viewModel.ReturnUrl);

      }
      ModelState.AddModelError(string.Empty, "Error occured");
      return View(viewModel);


    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoitId)
    {
      //мы используем интерекшн сервис чтобы получить логаут контекст и из него достать 
      //постлогаутредирект uri чтобы перейти
      await _signInManager.SignOutAsync();
      var logoutRequest = await _interactionService.GetLogoutContextAsync(logoitId);
      return Redirect(logoutRequest.PostLogoutRedirectUri);

    }

  }
}
