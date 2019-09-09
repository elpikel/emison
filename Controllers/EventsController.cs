using System;
using System.Linq;
using System.Threading.Tasks;
using Emison.Data;
using Emison.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emison.Controllers
{
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
        .Where(e => e.UserId == user.Id)
        .ToListAsync();

      if (events.Count > 1)
        return View("List", events.Select(e => new ViewModels.Event
        {
          Date = e.Date,
          Place = e.Place
        }).ToList());

      if (events.Count == 1)
      {
        var @event = events.First();
        return View("Details", new ViewModels.Event
        {
          Date = @event.Date,
          Place = @event.Place
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
    public IActionResult New()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> New([FromForm]NewEvent newEvent)
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
