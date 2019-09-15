using System;
using System.Collections.Generic;
using System.Text;
using Emison.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Emison.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public DbSet<Greeting> Greetings { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Book> Books { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
  }
}
