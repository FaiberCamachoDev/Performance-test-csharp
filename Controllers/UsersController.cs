using Microsoft.AspNetCore.Mvc;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Models;

namespace Performance_test_csharp.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    // GET users
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }
    
    //get: /User/Create
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.DocumentExistsAsync(user.Document))
            {
                ModelState.AddModelError("Document", "Document already exists.");
            }

            if (await _userService.EmailExistsAsync(user.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }

            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(user);
                return RedirectToAction("Index");
            }
        }
        return View(user);
    }
    
}