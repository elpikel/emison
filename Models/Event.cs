using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Emison.Models
{
  public class Event
  {
    [Key] public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Place { get; set; }
    public Guid InvitationCode { get; set; }
    [Required] public string UserId { get; set; }
    public virtual IdentityUser User { get; set; }
    public List<Greeting> Greetings { get; set; }
  }
}