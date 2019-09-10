using System;
using Microsoft.AspNetCore.Http;

namespace Emison.Guests.ViewModels
{
  public class NewGreeting
  {
    public string Text { get; set; }
    public string Signature { get; set; }
    public IFormFile File { get; set; }
    public Guid EventId { get; set; }
    public Guid InvitationCode { get; set; }
  }
}