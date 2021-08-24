using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StellarisPlanetList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace StellarisPlanetList.Data
{
    public class PlanetDB
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// Gets all planets even those in other users accounts
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public async static Task<int> GetTotalPlanetsAsync(ApplicationDbContext _context)
        {
            return await (from p in _context.Planets
                          select p).CountAsync();
        }

        /// <summary>
        /// Gets all planets that are linked to a certian user
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_httpContext"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public async static Task<List<PlanetViewModel>> GetPlanetsAsync(ApplicationDbContext _context, IHttpContextAccessor _httpContext, int pageSize, int pageNum)
        {
            int? userId = _httpContext.HttpContext.Session.GetInt32("UserId");
            string userName = null;

            if (userId.HasValue)
            {
                if (_httpContext.HttpContext.Session.GetString("Username") == null)
                {
                    userName = await (from u in _context.Users
                                      where u.UserId == userId
                                      select u.Username).SingleOrDefaultAsync();
                    _httpContext.HttpContext.Session.SetString("Username", userName);
                }
                else
                {
                    userName = _httpContext.HttpContext.Session.GetString("Username");
                }
            }

            return await (from p in _context.Planets
                          orderby p.PlanetName ascending
                          where p.UserId == userId
                          select p)
                       .Skip(pageSize * (pageNum - 1))
                       .Take(pageSize)
                       .ToListAsync();
        }

        /// <summary>
        /// simple add planet
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public async static Task<PlanetViewModel> AddPlanetAsync(ApplicationDbContext _context, PlanetViewModel p)
        {
            _context.Planets.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }

        /// <summary>
        /// Gets all planets linked to the logged in user
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_httpContext"></param>
        /// <param name="prodid"></param>
        /// <returns></returns>
        public static async Task<PlanetViewModel> GetPlanetAsync(ApplicationDbContext _context, IHttpContextAccessor _httpContext, int prodid)
        {
            int? userId = _httpContext.HttpContext.Session.GetInt32("UserId");
            string userName = null;

            if (userId.HasValue)
            {
                if (_httpContext.HttpContext.Session.GetString("Username") == null)
                {
                    userName = await (from u in _context.Users
                                      where u.UserId == userId
                                      select u.Username).SingleOrDefaultAsync();
                    _httpContext.HttpContext.Session.SetString("Username", userName);
                }
                else
                {
                    userName = _httpContext.HttpContext.Session.GetString("Username");
                }
            }

            PlanetViewModel p = await (from products in _context.Planets
                               where products.PlanetId == prodid && products.UserId == userId
                               select products).SingleAsync();

            return p;
        }
    }
}
