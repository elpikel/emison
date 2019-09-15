using System;
using System.Collections.Generic;

namespace Emison.Operators.ViewModels
{
  public class BookDetails
  {
    public long BookId { get; set; }
    public Guid EventId { get; set; }
    public List<Greeting> Greetings { get; set; }
  }
}