using System;
using System.Collections.Generic;

namespace Emison.Guests.ViewModels
{
  public class EventGreetings
  {
    public Guid EventId { get; set; }
    public Guid InvitationCode { get; set; }
    public List<Greeting> Greetings { get; set; }
  }
}