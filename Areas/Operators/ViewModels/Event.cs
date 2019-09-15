using System;
using System.Collections.Generic;

namespace Emison.Operators.ViewModels
{
  public class Event
  {
    public DateTime Date { get; set; }
    public string Place { get; set; }
    public Guid Id { get; set; }
    public Guid InvitationCode { get; set; }
    public List<Greeting> Greetings { get; set; }
  }
}