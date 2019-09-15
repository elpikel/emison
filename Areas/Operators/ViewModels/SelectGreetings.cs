using System;
using System.Collections.Generic;

namespace Emison.Operators.ViewModels
{
  public class SelectGreetings
  {
    public Guid EventId { get; set; }
    public List<long> Greetings { get; set; }
  }
}