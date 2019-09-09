using System.Threading.Tasks;
using System.Linq;
using Emison.Data;
using Emison.Models;
using Emison.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Emison.Controllers
{
  // todo: this should be moved to separate area?
  [AllowAnonymous]
  public class GreetingsController : Controller
  {
    private readonly ApplicationDbContext _db;

    public GreetingsController(ApplicationDbContext db)
    {
      _db = db;
    }

    public async Task<IActionResult> Index()
    {
      var greetings = await _db.Greetings.ToListAsync();

      return View(greetings.Select(g => new ViewModels.Greeting
      {
        Text = g.Text,
        Signature = g.Signature,
        File = g.File
      }).ToList());
    }

    public IActionResult New()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]NewGreeting newGreeting)
    {
      // todo add validation

      var file = await Download(newGreeting.File);
      _db.Greetings.Add(new Models.Greeting
      {
        Text = newGreeting.Text,
        Signature = newGreeting.Signature,
        File = file
      });

      await _db.SaveChangesAsync();

      return RedirectToAction("Index");
    }

    public async Task<string> Download(IFormFile fromFile)
    {
      var path = Path.Combine(
       Directory.GetCurrentDirectory(),
       "wwwroot/uploaded",
       fromFile.FileName);

      using (var stream = new FileStream(path, FileMode.Create))
        await fromFile.CopyToAsync(stream);

      return $"uploaded/{fromFile.FileName}";
    }
  }
}
