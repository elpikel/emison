using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Emison.Models
{
  public class Book
  {
    [Key] public long Id { get; set; }
    [Required] public Guid EventId { get; set; }
    [Required] public string UserId { get; set; }

    public IdentityUser User { get; set; }
    public Event Event { get; set; }
    public List<Greeting> Greetings { get; set; }
  }
}