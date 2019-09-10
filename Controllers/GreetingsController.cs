using System.Threading.Tasks;
using System.Linq;
using Emison.Data;
using Emison.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System;
using Emison.Guests.ViewModels;

namespace Emison.Controllers
{
  [AllowAnonymous]
  public class HOmeController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }
  }
}
