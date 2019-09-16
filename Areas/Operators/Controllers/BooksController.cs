using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
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
    private readonly IConverter _pdfConverter;

    public BooksController(
      ApplicationDbContext applicationDbContext,
      UserManager<IdentityUser> userManager,
      IConverter pdfConverter)
    {
      _applicationDbContext = applicationDbContext;
      _userManager = userManager;
      _pdfConverter = pdfConverter;
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
    public async Task<IActionResult> Create([FromForm]CreateBook createBook)
    {
      var greetings = await _applicationDbContext.Greetings
        .Where(g => createBook.Greetings.Any(sg => sg == g.Id))
        .ToListAsync();

      var currentDirectory = Directory.GetCurrentDirectory();

      var globalSettings = new GlobalSettings
      {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Portrait,
        PaperSize = PaperKind.A4,
        Margins = new MarginSettings { Top = 10 },
        DocumentTitle = "Book",
        Out = Path.Combine(currentDirectory, "wwwroot", "books", $"{createBook.BookId}.pdf")
      };

      var images = new StringBuilder();

      foreach (var greeting in greetings)
      {
        var path = Path.Combine(currentDirectory, "wwwroot") + greeting.File;
        images.Append($"<span>{greeting.Text}</span>{greeting.Signature}<span></span><img src=\"{path}\" />");
      }

      var objectSettings = new ObjectSettings
      {
        PagesCount = false,
        HtmlContent = $"<html><head></head><body>Test</body></html>",
        WebSettings = {
          DefaultEncoding = "utf-8"
          // UserStyleSheet = Path.Combine(currentDirectory, "wwwroot", "css", "book.css")
        }
      };

      var pdf = new HtmlToPdfDocument()
      {
        GlobalSettings = globalSettings,
        Objects = { objectSettings }
      };

      _pdfConverter.Convert(pdf);
      return View();
    }
  }
}
