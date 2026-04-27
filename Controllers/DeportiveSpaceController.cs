using Microsoft.AspNetCore.Mvc;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Models;

namespace Performance_test_csharp.Controllers;

public class DeportiveSpaceController : Controller
{
    private readonly IDeportiveSpace _sportService;

    public DeportiveSpaceController(IDeportiveSpace sportService)
    {
        _sportService = sportService;
    }
    // GET
    public async Task<IActionResult> Index(string? type)
    {
        var spaces = string.IsNullOrEmpty(type) ? await _sportService.GetAllAsync() : await _sportService.GetByTypeAsync(type);
        return View(spaces);
    }
    
    //get create
    public  IActionResult Create() => View();
    //le post
    [HttpPost]
    public async Task<IActionResult> Create(DeportiveSpace space)
    {
        if (ModelState.IsValid)
        {
            if (await _sportService.NameExistsAsync(space.Name))
            {
                ModelState.AddModelError("Name", "Duplicate deportive space name");
            }
            else
            {
                await _sportService.CreateAsync(space);
                return RedirectToAction(nameof(Index));
            }
        }
        return View(space);
    }
}