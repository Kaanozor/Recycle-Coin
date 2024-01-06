using BusinessLayer.Abstract;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Models;
using System.Security.Cryptography.X509Certificates;

namespace RecycleCoin.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        var user = _userService.GetUser(loginViewModel.Mail, loginViewModel.Password);
        if (user == null) { return View("Error"); }
        

        HttpContext.Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        });
        if (user.Auth == AuthEnum.Operator) { return Redirect("/Home/Operator"); }
        if (user.Auth == AuthEnum.Admin) { return Redirect("/Home/Admin"); }
        return Redirect("/Home/User");
    }
    public IActionResult LogOut()
    {
        HttpContext.Response.Cookies.Delete("userId");
        return Redirect("/");
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterViewModel registerViewModel)
    {

        if (_userService.UserGetUserByEmail(registerViewModel.Email) != null)
        {
            throw new Exception("Mail kullanýlmakta");
        }
        AuthEnum authType = AuthEnum.User;
        if (HttpContext.Request.Cookies.ContainsKey("userId"))
        {
            try
            {
                var userId = HttpContext.Request.Cookies["userId"];
                var user = _userService.UserGetUserById(Guid.Parse(userId));
                authType = user.Auth == AuthEnum.Admin ? AuthEnum.Operator : AuthEnum.User;
            }
            catch (Exception)
            {

            }
        }
        Random rnd = new Random();
        byte[] key= new byte[32];
        rnd.NextBytes(key);
        
        var keystr = Convert.ToBase64String(key);
        _userService.AddUser(new EntityLayer.Concrete.User
        {
            Name = registerViewModel.Name,
            Mail = registerViewModel.Email,
            PublicKey = keystr,
            Tc = registerViewModel.Tc,
            Password = registerViewModel.Password,
            Auth = authType
        });
        if (authType==AuthEnum.User)
        {
         
            return Login(new LoginViewModel { Mail = registerViewModel.Email, Password = registerViewModel.Password });
        }
        return Redirect("/Home/Admin");
    }
}
