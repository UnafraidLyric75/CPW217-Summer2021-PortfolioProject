using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarisPlanetList.Data;
using StellarisPlanetList.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StellarisPlanetList.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public PlanetsController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// How many planets are displayed per page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {

            int pageNum = id ?? 1;
            const int PageSize = 20;
            ViewData["CurrentPage"] = pageNum;

            int numProducts = await PlanetDB.GetTotalPlanetsAsync(_context);
            int totalPages = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["MaxPage"] = totalPages;

            List<PlanetViewModel> products = await PlanetDB.GetPlanetsAsync(_context, _httpContext, PageSize, pageNum);



            return View(products);
        }

        /// <summary>
        /// Adds a new planet to the planet lists
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PlanetViewModel p)
        {
            if (ModelState.IsValid)
            {
                p = await PlanetDB.AddPlanetAsync(_context, p);

                TempData["Message"] = $"{p.PlanetName} was added successfully";


                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// lets you edit a planet that is on your list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PlanetViewModel p = await PlanetDB.GetPlanetAsync(_context, _httpContext,id);
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlanetViewModel p)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                ViewData["Message"] = "Product updated successfully";
            }

            return View(p);
        }

        /// <summary>
        /// lets you delete a a planet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            PlanetViewModel p = await PlanetDB.GetPlanetAsync(_context, _httpContext, id);

            return View(p);
        }

        /// <summary>
        /// Confirms the planets deletion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PlanetViewModel p = await PlanetDB.GetPlanetAsync(_context, _httpContext, id);

            _context.Entry(p).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            TempData["Message"] = $"{p.PlanetName} was deleted";

            return RedirectToAction("Index");
        }
    }
}