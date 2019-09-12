using System.Threading.Tasks;
using System.Linq;
using Emison.Data;
using Emison.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Emison.Operators.Controllers
{
  [Area("Operators")]
  public class GreetingsController : Controller
  {
    private readonly ApplicationDbContext _db;

    public GreetingsController(ApplicationDbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid eventId)
    {
      var greetings = await _db.Greetings
        .Include(g => g.Event)
        .Where(g => g.EventId == eventId)
        .ToListAsync();

      return View(greetings
        .Select(g => new ViewModels.Greeting
        {
          Text = g.Text,
          Signature = g.Signature,
          File = g.File
        }).ToList());
    }
  }
}
