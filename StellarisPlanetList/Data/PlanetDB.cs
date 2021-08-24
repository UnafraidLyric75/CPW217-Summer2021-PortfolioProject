using Microsoft.AspNetCore.Mvc;
using System;
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
        public async static Task<int> GetTotalProductsAsync(ApplicationDbContext _context)
        {
            return await (from p in _context.Planets
                          select p).CountAsync();
        }

        public async static Task<List<PlanetViewModel>> GetProductsAsync(ApplicationDbContext _context, IHttpContextAccessor _httpContext, int pageSize, int pageNum)
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

        public async static Task<PlanetViewModel> AddProductAsync(ApplicationDbContext _context, PlanetViewModel p)
        {
            _context.Planets.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public static async Task<PlanetViewModel> GetProductAsync(ApplicationDbContext _context, IHttpContextAccessor _httpContext, int prodid)
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
