using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Helsi.Web.Models;
using Helsi.DataAccess;
using Helsi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Helsi.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HelsiContext _context;

        public HomeController(HelsiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var patients = _context.Set<Patient>()
                .Include(x => x.Gender)
                .Include(x => x.AdditionalContacts)
                .ThenInclude(x => x.ContactType)
                .ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
