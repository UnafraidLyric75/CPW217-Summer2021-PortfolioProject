using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StellarisPlanetList.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StellarisPlanetList.Controllers
{
    public class CookieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CookieController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public IActionResult Summary()
        {
            return View();
        }
    }
}
