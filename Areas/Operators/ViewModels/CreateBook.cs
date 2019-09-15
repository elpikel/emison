using System;
using System.Collections.Generic;

namespace Emison.Operators.ViewModels
{
  public class CreateBook
  {
    public long BookId { get; set; }
    public Guid EventId { get; set; }
    public List<long> Greetings { get; set; }
  }
}