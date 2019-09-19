using System;

namespace Emison.Operators.ViewModels
{
  public class FinishedBook
  {
    public long BookId { get; set; }
    public Guid EventId { get; set; }
    public string File { get; set; }
  }
}