using System;
using System.Linq;
using System.Threading.Tasks;
using Emison.Data;
using Emison.Operators.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emison.Operators.Controllers
{
  [Area("Operators")]
  public class EventsController : Controller
  {
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public EventsController(
      ApplicationDbContext applicationDbContext,
      UserManager<IdentityUser> userManager)
    {
      _applicationDbContext = applicationDbContext;
      _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var user = await _userManager.FindByNameAsync(User.Identity.Name);

      var events = await _applicationDbContext.Events
        .Include(e => e.Greetings)
        .Where(e => e.UserId == user.Id)
        .ToListAsync();

      if (events.Count > 1)
        return View("List", events.Select(e => new Operators.ViewModels.Event
        {
          Date = e.Date,
          Place = e.Place
        }).ToList());

      if (events.Count == 1)
      {
        var @event = events.First();
        return View("Details", new Operators.ViewModels.Event
        {
          Date = @event.Date,
          Place = @event.Place,
          Id = @event.Id,
          InvitationCode = @event.InvitationCode,
          Greetings = @event.Greetings.Select(g => new Operators.ViewModels.Greeting
          {
            Id = g.Id,
            Text = g.Text,
            Signature = g.Signature,
            File = g.File
          }).ToList()
        });
      }

      return View("New");
    }

    [HttpGet("/{eventId}")]
    public async Task<IActionResult> Index(Guid eventId)
    {
      var @event = await _applicationDbContext.Events
        .SingleOrDefaultAsync(e => e.Id == eventId);

      if (@event == null)
        return NotFound();

      return View(@event);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]NewEvent newEvent)
    {
      var user = await _userManager.FindByNameAsync(User.Identity.Name);

      _applicationDbContext.Events.Add(new Models.Event
      {
        Date = newEvent.Date,
        Place = newEvent.Place,
        UserId = user.Id,
        InvitationCode = Guid.NewGuid()
      });

      await _applicationDbContext.SaveChangesAsync();

      return RedirectToAction("Index");
    }
  }
}
