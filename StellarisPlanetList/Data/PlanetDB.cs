using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StellarisPlanetList.Models;
using Microsoft.EntityFrameworkCore;

namespace StellarisPlanetList.Data
{
    public class PlanetDB
    {
        public async static Task<int> GetTotalProductsAsync(ApplicationDbContext _context)
        {
            return await (from p in _context.Planets
                          select p).CountAsync();
        }

        public async static Task<List<PlanetViewModel>> GetProductsAsync(ApplicationDbContext _context, int pageSize, int pageNum)
        {
            return await (from p in _context.Planets
                          orderby p.PlanetName ascending
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

        public static async Task<PlanetViewModel> GetProductAsync(ApplicationDbContext context, int prodid)
        {
            PlanetViewModel p = await (from products in context.Planets
                               where products.PlanetId == prodid
                               select products).SingleAsync();

            return p;
        }
    }
}
