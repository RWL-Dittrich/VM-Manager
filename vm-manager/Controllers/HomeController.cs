using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using vm_manager.Models;
using vm_manager.Services;

namespace vm_manager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CommandService _commandService;
    private readonly JobService _jobService;
    public HomeController(ILogger<HomeController> logger, CommandService commandService, JobService jobService)
    {
        _logger = logger;
        _commandService = commandService;
        _jobService = jobService;
    }

    public async Task<IActionResult> Index()
    {
        // var connectionInfo = new Renci.SshNet.ConnectionInfo("10.2.10.2",
        //     "root",
        //     new PasswordAuthenticationMethod("root", "password"));
        //
        // _jobService.ScheduleJob(connectionInfo, new List<string>
        // {
        //     "apt update",
        // });
        
        
        return View();
    }
    
    [Route("/job/{jobId}/output")]
    public IActionResult GetJobOutput(int jobId)
    {
        var jobOutput = _jobService.GetJobOutput(jobId);
        return Json(new {output = jobOutput});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}