using System;

namespace Emison.Operators.ViewModels
{
  public class Event
  {
    public DateTime Date { get; set; }
    public string Place { get; set; }
    public Guid Id { get; set; }
    public Guid InvitationCode { get; set; }
  }
}