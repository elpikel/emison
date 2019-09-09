using System;
using System.ComponentModel.DataAnnotations;

namespace Emison.Models
{
  public class Greeting
  {
    [Key] public long Id { get; set; }
    public string Text { get; set; }
    public string Signature { get; set; }
    public string File { get; set; }
    [Required] public Guid EventId { get; set; }
    public Emison.Models.Event Event { get; set; }
  }
}