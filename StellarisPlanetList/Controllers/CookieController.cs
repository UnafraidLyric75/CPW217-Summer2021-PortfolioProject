using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StellarisPlanetList.Data;

namespace StellarisPlanetList.Controllers
{
    /// <summary>
    /// Only used to get seesion data to work
    /// </summary>
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
