using System.Diagnostics;
using BusinessLayer.Abstract;
using BusinessLayer.Helpers;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Models;

namespace RecycleCoin.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEntityRepository<Product> productRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

   

    public HomeController(ILogger<HomeController> logger,IEntityRepository<Product> entityRepository, IProductService productService, IUserService userService)
    {
        _logger = logger;
        productRepository = entityRepository;
        _productService = productService;
        _userService = userService;

    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    public IActionResult Admin()
    {
        return View();
    }
    [HttpPost]

    public IActionResult Admin(AdminViewModel adminViewModel)
    {
        _productService.AddProduct(new EntityLayer.Concrete.Product
        {
            TypeName = adminViewModel.TypeName,
            Carbon = adminViewModel.Carbon
        });
        return View();
    }

    public IActionResult Operator()
    {
        var model=new OperatorViewModel();
        model.Products = productRepository.GetAll();
        return View(model);
    }
    
    public async Task<IActionResult> User()
    {
        try
        {
            var userId = HttpContext.Request.Cookies["userId"];
            var user = _userService.UserGetUserById(Guid.Parse(userId));
            var amount = await NodejsHelper.GetAmount(user.PublicKey);
            return View(new UserViewModel
            {
                PublicKey = user.PublicKey,
                Amount = amount
            });
        }
        catch (Exception)
        {

        }
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}