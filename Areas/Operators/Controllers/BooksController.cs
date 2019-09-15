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
  public class BooksController : Controller
  {
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public BooksController(
      ApplicationDbContext applicationDbContext,
      UserManager<IdentityUser> userManager)
    {
      _applicationDbContext = applicationDbContext;
      _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> SelectGreetings([FromForm]SelectGreetings selectGreetings)
    {
      var userId = _userManager.GetUserId(User);

      var greetings = await _applicationDbContext.Greetings
        .Where(g => selectGreetings.Greetings.Any(sg => sg == g.Id))
        .ToListAsync();

      var newBook = new Models.Book
      {
        UserId = userId,
        EventId = selectGreetings.EventId,
        Greetings = greetings
      };

      _applicationDbContext.Books.Add(newBook);

      await _applicationDbContext.SaveChangesAsync();

      return View("Create", new BookDetails
      {
        BookId = newBook.Id,
        EventId = selectGreetings.EventId,
        Greetings = greetings.Select(g => new Greeting
        {
          Id = g.Id,
          File = g.File,
          Signature = g.Signature,
          Text = g.Text
        }).ToList()
      });
    }

    [HttpPost]
    public IActionResult SelectDetails([FromForm]SelectDetails selectDetails)
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm]CreateBook createBook)
    {

      return View();
    }
  }
}
