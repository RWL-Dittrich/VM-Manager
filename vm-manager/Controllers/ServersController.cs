using Microsoft.AspNetCore.Mvc;

namespace vm_manager.Controllers;

public class ServersController : Controller
{
    
    
    public IActionResult Index()
    {
        return View();
    }
}