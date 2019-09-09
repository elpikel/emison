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
using System;

namespace Emison.Controllers
{
  [AllowAnonymous]
  public class GreetingsController : Controller
  {
    private readonly ApplicationDbContext _db;

    public GreetingsController(ApplicationDbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid invitationCode, Guid eventId)
    {
      var greetings = await _db.Greetings
        .Include(g => g.Event)
        .Where(g => g.EventId == eventId && g.Event.InvitationCode == invitationCode)
        .ToListAsync();

      return View(new ViewModels.EventGreetings
      {
        EventId = eventId,
        InvitationCode = invitationCode,
        Greetings = greetings
        .Select(g => new ViewModels.Greeting
        {
          Text = g.Text,
          Signature = g.Signature,
          File = g.File
        }).ToList()
      });
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid invitationCode, Guid eventId)
    {
      var existingEvent = await _db.Events
        .SingleOrDefaultAsync(e => e.Id == eventId && e.InvitationCode == invitationCode);

      if (existingEvent != null)
        return View(new NewGreeting
        {
          EventId = eventId,
          InvitationCode = invitationCode
        });

      return Forbid();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]NewGreeting newGreeting)
    {
      var existingEvent = await _db.Events
        .SingleOrDefaultAsync(e => e.Id == newGreeting.EventId && e.InvitationCode == newGreeting.InvitationCode);

      if (existingEvent == null)
        return Forbid();
      // todo add validation

      var file = await Download(newGreeting.File);
      _db.Greetings.Add(new Models.Greeting
      {
        EventId = newGreeting.EventId,
        Text = newGreeting.Text,
        Signature = newGreeting.Signature,
        File = file
      });

      await _db.SaveChangesAsync();

      return RedirectToAction(
        "Index",
        "Greetings",
        new { InvitationCode = newGreeting.InvitationCode, EventId = newGreeting.EventId });
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
