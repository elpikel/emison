using Microsoft.AspNetCore.Http;

namespace Emison.ViewModels
{

  public class NewGreeting
  {
    public string Text { get; set; }
    public string Signature { get; set; }
    public IFormFile File { get; set; }
  }
}